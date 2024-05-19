using Microsoft.AspNetCore.Mvc;

namespace Anyar.Areas.Admin.ViewComponents
{
    public class NavHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
