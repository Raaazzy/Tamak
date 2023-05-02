using Tamak.Data.Models;

namespace Tamak.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> allProducts { get; set; }
        public string currentCategory { get; set; }
    }
}
