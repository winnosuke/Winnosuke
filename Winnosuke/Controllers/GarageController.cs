// ============================
// Controller: GarageController.cs (Rewritten to match IGarageService)
// ============================
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GarageController : Controller
    {
        private readonly IGarageService _garageService;

        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }

        public async Task<IActionResult> Index()
        {
            var garages = await _garageService.GetAllAsync();
            return View(garages);
        }

        public async Task<IActionResult> Details(int id)
        {
            var garage = await _garageService.GetByIdAsync(id);
            if (garage == null) return NotFound();
            return View(garage);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Garage garage)
        {
            if (!ModelState.IsValid) return View(garage);
            await _garageService.AddAsync(garage);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var garage = await _garageService.GetByIdAsync(id);
            if (garage == null) return NotFound();
            return View(garage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Garage garage)
        {
            if (!ModelState.IsValid) return View(garage);
            await _garageService.UpdateAsync(garage);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var garage = await _garageService.GetByIdAsync(id);
            if (garage == null) return NotFound();
            return View(garage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _garageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
