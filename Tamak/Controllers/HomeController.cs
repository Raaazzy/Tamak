using Microsoft.AspNetCore.Mvc;
using Tamak.Data.Interfaces;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService= productService;
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            var response = await _productService.GetProducts();
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = response.Data;
                return View(obj);
            }
            return RedirectToAction("Error");
        }
    }
}
