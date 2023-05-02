using Tamak.Data.Models;

namespace Tamak.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public ushort Price { get; set; }
        public bool Available { get; set; }
        public string Category { set; get; }
    }
}
