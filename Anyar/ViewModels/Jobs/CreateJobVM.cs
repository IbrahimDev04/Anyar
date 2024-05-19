using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Jobs
{
    public class CreateJobVM
    {
        [Required, MaxLength(64, ErrorMessage = "Lenght Error")]
        public string Name { get; set; }
    }
}
