using ForestWebUi.DTOs;
using ForestWebUi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestWebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            User user = new()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhotoUrl = "/image/avatar.png",
                UserName = registerDTO.Email,
                Email = registerDTO.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return View(registerDTO);
        }
    }
}
