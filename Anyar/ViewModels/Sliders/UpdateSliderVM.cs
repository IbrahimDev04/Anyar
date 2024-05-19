using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Sliders
{
    public class UpdateSliderVM
    {
        [Required, MaxLength(16, ErrorMessage = "Lenght Error")]
        public string Title { get; set; }

        [Required, MaxLength(64, ErrorMessage = "Lenght Error")]
        public string SubTitle { get; set; }
    }
}
