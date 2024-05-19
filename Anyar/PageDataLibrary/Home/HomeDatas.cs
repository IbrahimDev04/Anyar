using Anyar.ViewModels.Employees;
using Anyar.ViewModels.Jobs;
using Anyar.ViewModels.Products;
using Anyar.ViewModels.Services;
using Anyar.ViewModels.Sliders;

namespace Anyar.PageDataLibrary.Home
{
    public class HomeDatas
    {
        public List<GetSliderVM> GetSliderVM { get; set; }
        public List<GetServiceVM> GetServiceVM { get; set; }
        public List<GetProductVM> GetProductVM { get; set; }
        public List<GetEmployeeVM> GetEmployeeVM { get; set; }

    }
}
