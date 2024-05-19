using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Products
{
    public class CreateProductVM
    {
        [Required, MaxLength(16, ErrorMessage = "Lenght Error")]
        public string Title { get; set; }

        [Required, MaxLength(12, ErrorMessage = "Lenght Error")]
        public string Subtitle { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
