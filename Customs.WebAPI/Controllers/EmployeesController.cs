using System.Linq;
using System.Threading.Tasks;
using Customs.DAL.Models;
using Customs.DAL.Repositories;
using Customs.WebAPI.Helpers;
using Customs.WebAPI.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Customs.WebAPI.Controllers
{
    public class EmployeesController : Controller
    {
        private const int PageSize = 10;
        private readonly IBaseRepository<Employee> _employeeBaseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBaseRepository<Storage> _storageRepository;

        public EmployeesController(IBaseRepository<Employee> employeeBaseRepository,
            IBaseRepository<Storage> storageRepository,
            IEmployeeRepository employeeRepository)
        {
            _employeeBaseRepository = employeeBaseRepository;
            _storageRepository = storageRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var query = _employeeBaseRepository.GetEntities();
            if (searchString != null)
            {
                query = query
                    .Where(t => t.LastName.ToLower().Contains(searchString.ToLower().Trim()))
                    .Take(PageSize);
            }

            var entities = await query
                .ToListAsync();

            page ??= 1;
            var pagedItems = entities
                .ToPagedList(page.Value, PageSize);

            return View(pagedItems);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeBaseRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var storages = await _storageRepository.GetEntities()
                .ToListAsync();
            var employee = await _employeeBaseRepository.GetEntityById(id);

            var item = new UpdateEmployeeVm
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                IdNumber = employee.IdNumber,
                Role = employee.Role,
                StorageId = employee.EmployeeStorages.Select(x => x.StorageId).ToList(),
                Storages = storages.GetStoragesSelectedList()
            };

            ViewBag.Storages = storages;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeVm model)
        {
            var item = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                IdNumber = model.IdNumber,
                Role = model.Role,
                EmployeeStorages = model.StorageId.Select(id => new EmployeeStorage
                {
                    StorageId = id
                }).ToList()
            };

            await _employeeBaseRepository.Update(item);
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
        public async Task<IActionResult> Create(CreateEmployeeVm model)
        {
            var item = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                IdNumber = model.IdNumber,
                Role = model.Role,
                EmployeeStorages = model.StorageId.Select(s => new EmployeeStorage
                {
                    StorageId = s
                }).ToList()
            };

            await _employeeBaseRepository.Create(item);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("AutoComplete")]
        public async Task<JsonResult> AutoComplete(string text)
        {
            var distributors = await _employeeRepository.AutoCompleteEmployees(text);
            return Json(distributors);
        }

        [HttpGet]
        public async Task<JsonResult> GetEmployeesByStorageId(int storageId)
        {
            var employees = await _employeeRepository.GetEmployeesByStorageId(storageId);

            return Json(employees.GetEmployeesSelectedList());
        }
    }
}