using System.Drawing;

namespace Anyar.Models
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public int JobId { get; set; }
        public string ImageUrl { get; set; }
        public string TwitterName { get; set; }
        public string FacebookName { get; set; }
        public string InstagramName { get; set; }
        public string LinkedInName { get; set; }

        public Job Job { get; set; }

    }
}
