using Anyar.DataAccessLayer;
using Anyar.Extensions;
using Anyar.Models;
using Anyar.ViewModels.Employees;
using Anyar.ViewModels.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class EmployeeController(AnyarContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.employees
                .Select(s => new GetEmployeeAdminVM
                {
                    Name = s.Name,
                    Surname = s.Surname,
                    Description = s.Description,
                    ImageFile = s.ImageUrl,
                    TwitterName = s.TwitterName,
                    InstagramName = s.InstagramName,
                    Id = s.Id,
                    CreatedTime = s.CreatedTime.ToString("dd MMM yyyy"),
                    UpdatedTime = s.UpdateTime.ToString("dd MMM yyyy"),
                    FacebookName = s.FacebookName,
                    LinkedInName = s.LinkedInName,
                }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Jobs = await _context.jobs.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM vm)
        {
            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsValidType("image"))
                {
                    ModelState.AddModelError("ImageFile", "Type Error");
                }
                if (!vm.ImageFile.IsValidSize(200))
                {
                    ModelState.AddModelError("ImageFile", "Size Error");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string fileName = await vm.ImageFile.FileManagedAsync(Path.Combine(_env.WebRootPath, "imgs", "employees"));
            Employee employee = new Employee
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Description = vm.Description,
                InstagramName = vm.InstagramName,
                LinkedInName = vm.LinkedInName,
                TwitterName = vm.TwitterName,
                FacebookName = vm.FacebookName,
                ImageUrl = Path.Combine("imgs", "employees", fileName),
                JobId = vm.JobId,
            };

            await _context.employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.employees.FirstOrDefault(s  => s.Id == id);

            if(data == null) return NotFound();

            ViewBag.Jobs = await _context.jobs.ToListAsync();

            UpdateEmployeeVM vM = new UpdateEmployeeVM
            {
                Name = data.Name,
                Surname = data.Surname,
                Description = data.Description,
                InstagramName = data.InstagramName,
                LinkedInName = data.LinkedInName,
                TwitterName = data.TwitterName,
                FacebookName = data.FacebookName,
                JobId = data.JobId,
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateEmployeeVM vm)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.employees.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsValidType("image"))
                {
                    ModelState.AddModelError("ImageFile", "Type Error");
                }
                if (!vm.ImageFile.IsValidSize(200))
                {
                    ModelState.AddModelError("ImageFile", "Size Error");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string fileName = await vm.ImageFile.FileManagedAsync(Path.Combine(_env.WebRootPath, "imgs", "employees"));


            data.Name = vm.Name;
            data.Surname = vm.Surname;
            data.Description = vm.Description;
            data.InstagramName = vm.InstagramName;
            data.FacebookName = vm.FacebookName;
            data.JobId = vm.JobId;
            data.TwitterName = vm.TwitterName;
            data.FacebookName = vm.FacebookName;
            data.ImageUrl = Path.Combine("imgs", "employees",fileName);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = await _context.employees.FirstOrDefaultAsync(s => s.Id == id);

            if (data == null) return NotFound();

            data.ImageUrl.Delete(_env.ContentRootPath);

            _context.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
