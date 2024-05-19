using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Sliders
{
    public class CreateSliderVM
    {
        [Required,MaxLength(16, ErrorMessage = "Lenght Error")]
        public string Title { get; set; }

        [Required, MaxLength(64, ErrorMessage = "Lenght Error")]
        public string Subtitle { get; set; }
    }
}
