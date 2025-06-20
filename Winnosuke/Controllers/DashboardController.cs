using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IAdminDashboardService _dashboardService;

        public DashboardController(IAdminDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dashboardService.GetDashboardAsync();
            return View(model);
        }
    }
}
