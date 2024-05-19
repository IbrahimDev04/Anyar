using Anyar.DataAccessLayer;
using Anyar.Models;
using Anyar.PageDataLibrary.Home;
using Anyar.ViewModels.Employees;
using Anyar.ViewModels.Jobs;
using Anyar.ViewModels.Products;
using Anyar.ViewModels.Services;
using Anyar.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Controllers
{
    public class HomeController(AnyarContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var slider = await _context.sliders
                .Where(w => !w.IsDeleted)
                .Select(s => new GetSliderVM
                {
                    Title = s.Title,
                    Subtitle = s.SubTitle,
                }).ToListAsync();

            var service = await _context.services
                .Where(w => !w.IsDeleted)
                .Select(s => new GetServiceVM
                {
                    Title = s.Title,
                    Subtitle = s.SubTitle,
                    Icon = s.Icon,
                }).ToListAsync();

            var product = await _context.products
                .Where(w => !w.IsDeleted)
                .Select(s => new GetProductVM
                {
                    Title = s.Title,
                    Subtitle = s.SubTitle,
                    ImageFile = s.ImageFile,
                    Url = s.Url,
                }).ToListAsync();



            var employee = await _context.employees
                .Where(w => !w.IsDeleted)
                .Join(_context.jobs,
                        e => e.JobId,
                        j => j.Id,
                        (e, j) => new GetEmployeeVM
                        {
                            Name = e.Name,
                            Surname = e.Surname,
                            Description = e.Description,
                            ImageFile = e.ImageUrl,
                            FacebookName = e.FacebookName,
                            InstagramName = e.InstagramName,
                            LinkedInName = e.LinkedInName,
                            TwitterName = e.TwitterName,
                            JobId = j.Name,
                        }).ToListAsync();
                

            HomeDatas datas = new HomeDatas
            {
                GetSliderVM = slider,
                GetServiceVM = service,
                GetProductVM = product,
                GetEmployeeVM = employee,
            };
            return View(datas);
        }
    }
}
