// ============================
// Controller: AdminActivityController.cs (Rewritten to match IAdminActivityService)
// ============================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminActivityController : Controller
    {
        private readonly IAdminActivityService _adminActivityService;

        public AdminActivityController(IAdminActivityService adminActivityService)
        {
            _adminActivityService = adminActivityService;
        }

        public async Task<IActionResult> Index()
        {
            var activities = await _adminActivityService.GetAllAsync();
            return View(activities);
        }

        public async Task<IActionResult> Details(int id)
        {
            var activity = await _adminActivityService.GetByIdAsync(id);
            if (activity == null) return NotFound();
            return View(activity);
        }
    }
}
