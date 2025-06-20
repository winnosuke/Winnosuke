using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    public class TechnicalReportController : Controller
    {
        private readonly ITechnicalReportService _reportService;

        public TechnicalReportController(ITechnicalReportService reportService)
        {
            _reportService = reportService;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [Authorize(Roles = "Technician")]
        public async Task<IActionResult> MyReports()
        {
            var techId = GetCurrentUserId();
            var reports = await _reportService.GetByTechnicianIdAsync(techId);
            return View(reports);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MyVehicleReports()
        {
            var userId = GetCurrentUserId();
            var reports = await _reportService.GetByUserIdAsync(userId);
            return View(reports);
        }

        [Authorize(Roles = "Technician")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Technician")]
        public async Task<IActionResult> Create(TechnicalReport model)
        {
            model.TechnicianId = GetCurrentUserId();
            model.CreatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                await _reportService.CreateAsync(model);
                return RedirectToAction(nameof(MyReports));
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report == null)
                return NotFound();

            var userId = GetCurrentUserId();
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (role == "Technician" && report.TechnicianId != userId)
                return Forbid();
            if (role == "Customer" && report.Appointment.UserId != userId)
                return Forbid();

            return View(report);
        }
    }
}
