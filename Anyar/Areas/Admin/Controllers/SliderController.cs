using Anyar.DataAccessLayer;
using Anyar.ViewModels.Sliders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Member")]
    public class SliderController(AnyarContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.sliders
                .Select(s => new GetSliderAdminVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    Subtitle = s.SubTitle,
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
        public async Task<IActionResult> Create(CreateSliderVM vm)
        {
            await _context.sliders.AddAsync(new Models.Slider
            {
                Title = vm.Title,
                SubTitle = vm.Subtitle,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.sliders.FirstOrDefault(s=>s.Id==id);

            if (data == null) NotFound();

            UpdateSliderVM swm = new UpdateSliderVM
            {
                Title = data.Title,
                SubTitle = data.SubTitle
            };

            return View(swm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, CreateSliderVM vm)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.sliders.FirstOrDefault(s => s.Id == id);

            if (data == null) NotFound();

            data.Title = vm.Title;
            data.SubTitle = vm.Subtitle;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.sliders.FirstOrDefault(s => s.Id == id);

            if (data == null) NotFound();

            _context.Remove(data);  

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
