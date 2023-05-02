using Microsoft.AspNetCore.Mvc;
using Tamak.Data.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _allProducts;
        private readonly IProductsCategory _allCategories;

        public HomeController(IProductRepository allProducts, IProductsCategory allCategories)
        {
            _allProducts = allProducts;
            _allCategories = allCategories;
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            HomeViewModel obj = new HomeViewModel();
            obj.allProducts = await _allProducts.Select();
            return View(obj);
        }
    }
}
