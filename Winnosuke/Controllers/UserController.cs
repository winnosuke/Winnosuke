// ============================
// Controller: UserController.cs (Rewritten to match IUserService)
// ============================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid) return View(user);
            await _userService.AddAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid) return View(user);
            await _userService.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
