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
        public async Task<ActionResult> GetUser()
        {
            var userEmail = User.Identity.Name;
            var response = await _userService.GetUser(userEmail);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            ModelState.Remove("Role");
            ModelState.Remove("Id");
            if (User.IsInRole("User"))
            {
                ModelState.Remove("Campus");
            } else if (User.IsInRole("Admin") && model.City != "Moscow")
            {
                model.Campus = "None";
            }
            if (ModelState.IsValid)
            {
                var response = await _userService.Save(model);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return Json(new { data = response.Description });
                }
            }
            return RedirectToAction("GetProducts");
        }
    }
}
