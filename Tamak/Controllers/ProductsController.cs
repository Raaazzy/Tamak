using Microsoft.AspNetCore.Mvc;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IAllProducts _allProducts;
        private readonly IProductsCategory _allCategories;

        public ProductsController(IAllProducts allProducts, IProductsCategory allCategories)
        {
            _allProducts = allProducts;
            _allCategories = allCategories;
        }

        public ViewResult List(string category)
        {
            ProductsListViewModel obj = new ProductsListViewModel();
            obj.allProducts = _allProducts.Products;
            return View(obj);
        }
    }
}
