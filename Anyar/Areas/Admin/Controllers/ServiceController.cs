using Anyar.DataAccessLayer;
using Anyar.ViewModels.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class ServiceController(AnyarContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.services
                .Select(s => new GetServiceAdminVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    Subtitle = s.SubTitle,
                    Icon = s.Icon,
                    CreatedTime = s.CreatedTime.ToString("dd MMM yyyy"),
                    UpdatedTime = s.UpdateTime.ToString("dd MMM yyyy"),
                }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM vm)
        {
            await _context.services.AddAsync(new Models.Service
            {
                Title = vm.Title,
                SubTitle = vm.Subtitle,
                Icon = vm.Icon,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null || id<0) BadRequest();

            var data = _context.services.FirstOrDefault(s => s.Id == id);

            if(data == null) NotFound();

            UpdateServiceVM vM = new UpdateServiceVM
            {
                Title = data.Title,
                Subtitle = data.SubTitle,
                Icon = data.Icon,
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateServiceVM vm)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.services.FirstOrDefault(s => s.Id == id);

            if (data == null) NotFound();

            data.Title = vm.Title;
            data.SubTitle = vm.Subtitle;
            data.Icon = vm.Icon;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.services.FirstOrDefault(s => s.Id == id);

            if (data == null) NotFound();

            _context.Remove(data);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
