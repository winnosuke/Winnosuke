using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    public class AccountRegisterController : Controller
    {
        private readonly IAuthService _authService;

        public AccountRegisterController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authService.RegisterAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Email hoặc tên đăng nhập đã tồn tại.");
                return View(model);
            }

            return RedirectToAction("EmailSent");
        }

        public IActionResult EmailSent() => View();

        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var result = await _authService.ConfirmEmailAsync(token);
            return View(result ? "ConfirmSuccess" : "ConfirmFailed");
        }

        [HttpGet]
        public IActionResult ResendConfirmation() => View();

        [HttpPost]
        public async Task<IActionResult> ResendConfirmation(string email)
        {
            await _authService.ResendConfirmationAsync(email);
            return View("EmailSent");
        }
    }
}
