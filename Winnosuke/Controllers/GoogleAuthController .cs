using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Winnosuke.Controllers
{
    [AllowAnonymous]
    public class GoogleAuthController : Controller
    {
        [HttpGet("~/login-google")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("GoogleResponse", "GoogleAuth");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims;

            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // Kiểm tra email trong DB
            // Nếu chưa có, tạo tài khoản mới
            // Sau đó tạo ClaimsIdentity và SignIn

            return RedirectToAction("Index", "Home");
        }
    }

}
