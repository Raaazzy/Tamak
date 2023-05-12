using System.ComponentModel.DataAnnotations;
using Tamak.Data.Models;

namespace Tamak.ViewModels
{
    public class CreateOrderViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Время заказа")]
        public long OrderDate { get; set; }

        public long ProductId { get; set; }

        public string Login { get; set; }

        public string Shop { get; set; }

        public string City { get; set; }

        public string Campus { get; set; }

        public string Status { get; set; }

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Assortiment> Assortiments { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Time> Times { get; set; }
    }
}
