using Anyar.DataAccessLayer;
using Anyar.Extensions;
using Anyar.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anyar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    public class ProductController(AnyarContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.products
                .Select(s => new GetProductAdminVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    Subtitle = s.SubTitle,
                    ImageFile = s.ImageFile,
                    Url = s.Url,
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
        public async Task<IActionResult> Create(CreateProductVM vm)
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

            string fileName = await vm.ImageFile.FileManagedAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));

            await _context.products.AddAsync(new Models.Product
            {
                Title = vm.Title,
                SubTitle = vm.Subtitle,
                ImageFile = Path.Combine("imgs", "products", fileName),
                Url = vm.Url
            });

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.products.FirstOrDefault(s => s.Id == id);

            if(data == null) NotFound();

            UpdateProductVM vm = new UpdateProductVM
            {
                Title = data.Title,
                Subtitle = data.SubTitle,
                Url = data.Url
            };

            return View(vm);


        }

        [HttpPost]
        public async Task<IActionResult> Update(int?id, UpdateProductVM vm)
        {

            if (id == null || id < 0) BadRequest();

            var data = _context.products.FirstOrDefault(s => s.Id == id);

            if (data == null) NotFound();

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

            string fileName = await vm.ImageFile.FileManagedAsync(Path.Combine(_env.WebRootPath, "imgs", "products"));

            data.Title = vm.Title;
            data.SubTitle = vm.Subtitle;
            data.Url = vm.Url;
            data.ImageFile = Path.Combine("imgs","products",fileName);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
