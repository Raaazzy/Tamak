using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Tamak.Data.Enum;
using Tamak.Data.Extensions;
using Tamak.Data.Models;
using Tamak.Data.Repository;
using Tamak.Service.Implementations;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IAssortimentService _assortimentService;
        private readonly IAssortimentService _timeService;

        public OrderController(IUserService userService, IProductService productService, IAssortimentService assortimentService, IAssortimentService timeService, IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _userService = userService;
            _productService = productService;
            _assortimentService = assortimentService;
            _timeService = timeService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder(long id)
        {
            var response = await _userService.GetUsers();
            var response1 = _productService.GetProducts();
            var response2 = _assortimentService.GetAssortiments();
            var response3 = _assortimentService.GetTimes();

            string shopEmail = "";
            string shopCity = "";
            string shopCampus = "";
            foreach (var product in response1.Data)
            {
                if (product.Id == id)
                {
                    foreach (var it in response2.Data)
                    {
                        if (it.Id == product.AssortimentId)
                        {
                            foreach (var user in response.Data)
                            {
                                if (it.UserId == user.Id)
                                {
                                    shopEmail = user.Email;
                                    shopCity = user.City.GetDisplayName();
                                    shopCampus = user.Campus.GetDisplayName();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            var orderModel = new CreateOrderViewModel()
            {
                ProductId = id,
                Login = User.Identity.Name,
                Shop = shopEmail,
                City = shopCity,
                Campus = shopCampus,
                Users = response.Data,
                Products = response1.Data,
                Assortiments = response2.Data,
                Status = "Process",
                Times = response3.Data
            };
            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            ModelState.Remove("Assortiments");
            ModelState.Remove("Users");
            ModelState.Remove("Products");
            ModelState.Remove("Times");

            if (ModelState.IsValid)
            {
                var response = await _orderService.Create(model);

                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _orderService.Delete(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(long id)
        {
            var responseOrder = await _orderService.ChangeStatus(id);
            return RedirectToAction("Detail", "Basket");
        }
    }
}
