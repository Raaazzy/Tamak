using Microsoft.AspNetCore.Mvc;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShopCart _shopCart;

        public ShopCartController(IProductRepository productRepository, ShopCart shopCart)
        {
            _productRepository = productRepository;
            _shopCart = shopCart;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var items = _shopCart.getShopItems();
            _shopCart.listShopItems = items;
            var obj = new ShopCartViewModel
            {
                shopCart = _shopCart
            };
            return View(obj);
        }

        public RedirectToActionResult addToCart(int id)
        {
            var item = _productRepository.Get(id);
            if (item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }
    }
}
