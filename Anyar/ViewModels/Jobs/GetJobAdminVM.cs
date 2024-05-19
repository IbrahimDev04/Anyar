using System.ComponentModel.DataAnnotations;

namespace Anyar.ViewModels.Jobs
{
    public class GetJobAdminVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
    }
}
