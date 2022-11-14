using System;
using System.Linq;
using System.Threading.Tasks;
using Customs.DAL.Models;
using Customs.DAL.Repositories;
using Customs.WebAPI.Helpers;
using Customs.WebAPI.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Customs.WebAPI.Controllers
{
    public class ProductsController : Controller
    {
        private const int PageSize = 10;
        private readonly IBaseRepository<Product> _productBaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBaseRepository<Storage> _storageRepository;

        public ProductsController(IBaseRepository<Product> productBaseRepository,
            IBaseRepository<Storage> storageRepository,
            IProductRepository productRepository)
        {
            _productBaseRepository = productBaseRepository;
            _storageRepository = storageRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var query = _productBaseRepository.GetEntities();
            if (searchString != null)
            {
                query = query
                    .Where(t => t.Name.ToLower().Contains(searchString.ToLower().Trim()))
                    .Take(PageSize);
            }

            var entities = await query
                .ToListAsync();

            var products = entities.Select(product => new ResultProductVm
            {
                Id = product.Id,
                Name = product.Name,
                StorageName = product.Storage.Name,
                UnitMeasurement = product.UnitMeasurement,
                Duty = product.Duty != null
                    ? Math.Round(
                        product.Duty.DutyForAllProducts / product.Duty.AmountOfReceipt * product.UnitMeasurement, 2)
                    : 0
            });

            page ??= 1;
            var pagedItems = products
                .ToPagedList(page.Value, PageSize);

            return View(pagedItems);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _productBaseRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var storages = await _storageRepository.GetEntities()
                .ToListAsync();
            var product = await _productBaseRepository.GetEntityById(id);

            var item = new UpdateProductVm
            {
                Id = product.Id,
                Name = product.Name,
                UnitMeasurement = product.UnitMeasurement,
                StorageId = product.StorageId,
                Storages = storages.GetStoragesSelectedList()
            };

            ViewBag.Storages = storages;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVm model)
        {
            var item = new Product
            {
                Id = model.Id,
                Name = model.Name,
                UnitMeasurement = model.UnitMeasurement,
                StorageId = model.StorageId
            };

            await _productBaseRepository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var storages = await _storageRepository.GetEntities()
                .ToListAsync();

            ViewBag.Storages = storages;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm model)
        {
            var item = new Product
            {
                Name = model.Name,
                UnitMeasurement = model.UnitMeasurement,
                StorageId = model.StorageId
            };

            await _productBaseRepository.Create(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetProductsByStorageId(int storageId)
        {
            var products = await _productRepository.GetProductsByStorageId(storageId);

            return Json(products.GetProductsSelectedList());
        }
    }
}