namespace Tamak.Data.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public uint price { get; set; }
        public virtual Product product { get; set; }

    }
}
