using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tamak.Data.Response;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Detail()
        {
            IBaseResponse<IEnumerable<OrderViewModel>> response;
            if (User.IsInRole("Admin"))
            {
                response = await _basketService.GetShopItems(User.Identity.Name);
            } else
            {
                response = await _basketService.GetItems(User.Identity.Name);
            }
            return View(response.Data.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(long id)
        {
            var response = await _basketService.GetItem(User.Identity.Name, id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return PartialView(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
