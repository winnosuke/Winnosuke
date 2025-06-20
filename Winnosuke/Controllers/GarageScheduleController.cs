using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GarageScheduleController : Controller
    {
        private readonly IGarageScheduleService _scheduleService;

        public GarageScheduleController(IGarageScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _scheduleService.GetAllAsync();
            return View(schedules);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GarageSchedule model)
        {
            if (ModelState.IsValid)
            {
                await _scheduleService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            if (schedule == null) return NotFound();

            return View(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GarageSchedule model)
        {
            if (ModelState.IsValid)
            {
                var updated = await _scheduleService.UpdateAsync(model);
                if (!updated) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _scheduleService.GetByIdAsync(id);
            if (schedule == null) return NotFound();

            return View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _scheduleService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
