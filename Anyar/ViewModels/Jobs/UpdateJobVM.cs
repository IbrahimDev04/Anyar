using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Jobs
{
    public class UpdateJobVM
    {
        [Required, MaxLength(64, ErrorMessage = "Lenght Error")]
        public string Name { get; set; }
    }
}
