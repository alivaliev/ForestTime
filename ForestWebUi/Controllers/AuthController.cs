using ForestWebUi.DTOs;
using ForestWebUi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForestWebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var findUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (findUser == null)
            {
                return RedirectToAction("Login");
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(findUser,loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                string c = _contextAccessor.HttpContext.Request.Query["controller"];
                string a = _contextAccessor.HttpContext.Request.Query["action"];
                string i = _contextAccessor.HttpContext.Request.Query["id"];
                if (!string.IsNullOrWhiteSpace(c))
                {
                    return RedirectToAction(a, c, new { Id = i });
                }                
                return RedirectToAction("Index", "Home");
            }
            return View(loginDTO);
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
