using Automarket.Service.Implementations;
using Microsoft.AspNetCore.Mvc;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> GetUser(int id)
        {
            var response = await _userService.GetUser(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id, bool isJson)
        {
            var response = await _userService.GetUser(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetUser", response.Data);
        }
    }
}
