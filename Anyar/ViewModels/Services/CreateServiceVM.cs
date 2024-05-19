using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Services
{
    public class CreateServiceVM
    {
        [Required, MaxLength(16, ErrorMessage = "Lenght Error")]
        public string Title { get; set; }

        [Required, MaxLength(64, ErrorMessage = "Lenght Error")]
        public string Subtitle { get; set; }

        [Required]
        public string Icon { get; set; }
    }
}
