using System.Linq;
using System.Threading.Tasks;
using Customs.DAL.Models;
using Customs.DAL.Repositories;
using Customs.WebAPI.Models.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Customs.WebAPI.Controllers
{
    public class StoragesController : Controller
    {
        private const int PageSize = 10;
        private readonly IBaseRepository<Storage> _repository;

        public StoragesController(IBaseRepository<Storage> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var query = _repository.GetEntities();
            if (searchString != null)
            {
                query = query
                    .Where(t => t.Name.ToLower().Contains(searchString.ToLower().Trim()))
                    .Take(PageSize);
            }

            var entities = await query
                .ToListAsync();

            var storages = entities.Select(storage => new ResultStorageVm
            {
                Id = storage.Id,
                Name = storage.Name,
                Products = storage.Products.Select(product => product.Name).ToList()
            });

            page ??= 1;
            var pagedItems = storages
                .ToPagedList(page.Value, PageSize);

            return View(pagedItems);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _repository.GetEntityById(id);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Storage item)
        {
            await _repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Storage item)
        {
            await _repository.Create(item);

            return RedirectToAction(nameof(Index));
        }
    }
}