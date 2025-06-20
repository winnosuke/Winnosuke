// ============================
// Controller: RepairStatusController.cs (Rewritten to match IRepairStatusService)
// ============================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Technician,Admin")]
    public class RepairStatusController : Controller
    {
        private readonly IRepairStatusService _repairStatusService;

        public RepairStatusController(IRepairStatusService repairStatusService)
        {
            _repairStatusService = repairStatusService;
        }

        public async Task<IActionResult> ByAppointment(int appointmentId)
        {
            var statusList = await _repairStatusService.GetByAppointmentIdAsync(appointmentId);
            ViewBag.AppointmentId = appointmentId;
            return View(statusList);
        }

        public IActionResult Create(int appointmentId)
        {
            var model = new RepairStatus { AppointmentId = appointmentId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepairStatus status)
        {
            if (!ModelState.IsValid) return View(status);
            await _repairStatusService.AddAsync(status);
            return RedirectToAction("ByAppointment", new { appointmentId = status.AppointmentId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var status = await _repairStatusService.GetByIdAsync(id);
            if (status == null) return NotFound();
            return View(status);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _repairStatusService.GetByIdAsync(id);
            if (status == null) return NotFound();

            await _repairStatusService.DeleteAsync(id);
            return RedirectToAction("ByAppointment", new { appointmentId = status.AppointmentId });
        }
    }
}
