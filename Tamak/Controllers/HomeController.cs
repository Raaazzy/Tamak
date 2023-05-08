using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
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
            _productService = productService;
        }

        [HttpGet]
        public IActionResult IndexAsync()
        {
            var response = _productService.GetProducts();
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = response.Data;
                return View(obj);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<ActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = (IEnumerable<Data.Models.Product>)response.Data;
                return View(obj);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return RedirectToAction("GetProducts");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = (IEnumerable<Data.Models.Product>)response.Data;
                return View(obj);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            ModelState.Remove("Avatar");
            ModelState.Remove("Img");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                var response = await _productService.Save(model);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return Json(new { data = response.Description });
                }
            }
            return RedirectToAction("GetProducts");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAvaliable(ProductViewModel model)
        {
            ModelState.Remove("Avatar");
            ModelState.Remove("Img");
            ModelState.Remove("Category");
            ModelState.Remove("Name");
            ModelState.Remove("Description");
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                var response = await _productService.ChangeAvaliable(model);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return Json(new { data = response.Description });
                }
            }
            return RedirectToAction("GetProducts");
        }
    }
}
