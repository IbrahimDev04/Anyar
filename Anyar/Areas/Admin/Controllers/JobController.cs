using Anyar.DataAccessLayer;
using Anyar.ViewModels.Jobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class JobController(AnyarContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.jobs
                .Select(s => new GetJobAdminVM
                {
                    Name = s.Name,
                    Id = s.Id,
                    CreatedTime = s.CreatedTime.ToString("dd MMM yyyy"),
                    UpdatedTime = s.UpdateTime.ToString("dd MMM yyyy")
                }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateJobVM vm)
        {

            await _context.jobs.AddAsync(new Models.Job
            {
                Name = vm.Name,
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) return BadRequest();

            var data = _context.jobs.FirstOrDefault(s => s.Id == id);

            if(data == null) return NotFound();

            UpdateJobVM vM = new UpdateJobVM
            {
                Name = data.Name
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateJobVM vm)
        {
            if (id == null || id < 0) return BadRequest();

            var data = _context.jobs.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            data.Name = vm.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || id < 0) return BadRequest();

            var data = _context.jobs.FirstOrDefault(s => s.Id == id);

            if (data == null) return NotFound();

            _context.jobs.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
