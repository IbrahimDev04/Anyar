using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Employees
{
    public class CreateEmployeeVM
    {
        [Required, MaxLength(16, ErrorMessage = "Lenght Error")]
        public string Name { get; set; }

        [Required, MaxLength(12, ErrorMessage = "Lenght Error")]
        public string Surname { get; set; }

        [Required, MaxLength(12, ErrorMessage = "Lenght Error")]
        public string Description { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string TwitterName { get; set; }

        [Required]
        public string FacebookName { get; set; }

        [Required]
        public string InstagramName { get; set; }

        [Required]
        public string LinkedInName { get; set; }

    }
}
