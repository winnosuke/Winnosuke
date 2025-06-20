using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RevenueController : Controller
    {
        private readonly IRevenueService _revenueService;

        public RevenueController(IRevenueService revenueService)
        {
            _revenueService = revenueService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _revenueService.GetRevenueAsync();
            return View(data);
        }
    }
}
