using Tamak.Data.Enum;

namespace Tamak.Data.Models
{
    public class Order
    {
        public long Id { get; set; }

        public long? ProductId { get; set; }

        public string OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public long? BasketId { get; set; }

        public virtual Basket Basket { get; set; }

        public string ShopEmail { get; set; }
    }
}
