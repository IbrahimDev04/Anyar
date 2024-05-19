using Anyar.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;


namespace Anyar.Areas.Admin.ViewComponents
{
    public class HeaderAdminViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View();
        }
    }
}
