using Tamak.Data.Models;
using Tamak.Data.Response;

namespace Tamak.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> allProducts { get; set; }

        public LoginViewModel loginViewModel { get; set; }
    }
}