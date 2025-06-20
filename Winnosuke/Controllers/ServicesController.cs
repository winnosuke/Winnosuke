using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Models;
using Winnosuke.Services;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // Public view
        [AllowAnonymous]
        public async Task<IActionResult> PublicList()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }

        // Admin view
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminList()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Service model)
        {
            if (ModelState.IsValid)
            {
                await _serviceService.CreateAsync(model);
                return RedirectToAction(nameof(AdminList));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Service model)
        {
            if (ModelState.IsValid)
            {
                var result = await _serviceService.UpdateAsync(model);
                if (!result) return NotFound();
                return RedirectToAction(nameof(AdminList));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _serviceService.DeleteAsync(id);
            return RedirectToAction(nameof(AdminList));
        }
    }
}
