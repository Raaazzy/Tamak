namespace Tamak.Data.Models
{
    public class Assortiment
    {
        public long Id { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public List<Product> Products { get; set; }
    }
}
