namespace Anyar.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageFile { get; set; }
        public string Url { get; set; }
    }
}
