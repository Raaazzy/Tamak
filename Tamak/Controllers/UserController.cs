using Microsoft.AspNetCore.Mvc;
using Tamak.Service.Interfaces;

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
    }
}
