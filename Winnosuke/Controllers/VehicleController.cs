using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Customer")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var vehicles = await _vehicleService.GetVehiclesByUserIdAsync(userId);
            return View(vehicles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle model)
        {
            model.UserId = GetCurrentUserId();

            if (ModelState.IsValid)
            {
                await _vehicleService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null || vehicle.UserId != GetCurrentUserId())
                return NotFound();

            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehicle model)
        {
            model.UserId = GetCurrentUserId();

            if (ModelState.IsValid)
            {
                var updated = await _vehicleService.UpdateAsync(model);
                if (!updated) return NotFound();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null || vehicle.UserId != GetCurrentUserId())
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null || vehicle.UserId != GetCurrentUserId())
                return NotFound();

            return View(vehicle);
        }
    }
}
