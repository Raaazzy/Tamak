using Tamak.Data.Enum;

namespace Tamak.Data.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Img { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public int CategoryId { get; set; }
        public Category Category { set; get; }
        public byte[]? Avatar { get; set; }

        public long AssortimentId { set; get; }
        public virtual Assortiment Assortiment { set; get; }

    }
}
