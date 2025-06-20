using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Winnosuke.Models;
using Winnosuke.Models;
using Winnosuke.Services;
using Winnosuke.Services;
using Winnosuke.ViewModels;

namespace Winnosuke.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;


        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment model)
        {
            if (ModelState.IsValid)
            {
                await _appointmentService.CreateAppointmentAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        [Authorize(Roles = "Admin,Technician,Customer")]
        public async Task<IActionResult> Search(AppointmentSearchViewModel filter)
        {
            var result = await _appointmentService.SearchAsync(filter);
            ViewBag.Filter = filter; // dùng để giữ lại input đã nhập
            return View("Search", result);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment model)
        {
            if (id != model.AppointmentId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var result = await _appointmentService.UpdateAppointmentAsync(model);
                if (!result)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
