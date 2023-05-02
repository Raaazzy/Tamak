using Microsoft.AspNetCore.Mvc;
using Tamak.Data.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllProducts _allProducts;
        private readonly IProductsCategory _allCategories;

        public HomeController(IAllProducts allProducts, IProductsCategory allCategories)
        {
            _allProducts = allProducts;
            _allCategories = allCategories;
        }

        public ViewResult Index()
        {
            HomeViewModel obj = new HomeViewModel();
            obj.allProducts = _allProducts.Products;
            return View(obj);
        }
    }
}
