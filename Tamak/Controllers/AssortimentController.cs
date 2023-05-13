using Microsoft.AspNetCore.Mvc;
using Tamak.Service.Interfaces;

namespace Tamak.Controllers
{
    public class AssortimentController : Controller
    {
        private readonly IAssortimentService _assortimentService;

        public AssortimentController(IAssortimentService basketService)
        {
            _assortimentService = basketService;
        }

        public async Task<IActionResult> Detail()
        {
            var response = await _assortimentService.GetItems(User.Identity.Name);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(long id)
        {
            var response = await _assortimentService.GetItem(User.Identity.Name, id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return PartialView(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
