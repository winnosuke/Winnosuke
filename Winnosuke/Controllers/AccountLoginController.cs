using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    public class AccountLoginController : Controller
    {
        private readonly IAuthService _authService;
        public AccountLoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _authService.AuthenticateAsync(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Sai thông tin hoặc chưa xác nhận email.");
                return View(model);
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }

}
