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
        private readonly IHttpContextAccessor _httpContext;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
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
                string c = _httpContext.HttpContext.Request.Query["controller"].ToString();
                string a = _httpContext.HttpContext.Request.Query["action"].ToString();
                string i = _httpContext.HttpContext.Request.Query["id"].ToString();
                if (!string.IsNullOrWhiteSpace(c))
                {
                    return RedirectToAction(a, c, new { id = i });
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
