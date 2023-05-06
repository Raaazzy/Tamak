using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Tamak.Data.Interfaces;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var response = _productService.GetProducts();
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = response.Data;
                return View(obj);
            }
            return RedirectToAction("Error", $"{response.Description}");
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
            return RedirectToAction("Error", $"{response.Description}");
        }

        public IActionResult Compare() => PartialView();

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return PartialView();
            }

            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = (IEnumerable<Data.Models.Product>)response.Data;
                return View(obj);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    } 
                    await _productService.Create(model, imageData);
                }
                else
                {
                    await _productService.Edit(model.Id, model);
                }
                return RedirectToAction("GetProducts");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetCategories()
        {
            var categories = _productService.GetCategories();
            return Json(categories.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetProduct(string term)
        {
            var response = await _productService.GetProduct(term);
            return Json(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id, bool isJson)
        {
            var response = await _productService.GetProduct(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetCar", response.Data);
        }

    }
}
