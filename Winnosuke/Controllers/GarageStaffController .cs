using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;

namespace Winnosuke.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GarageStaffController : Controller
    {
        private readonly MyGarageFinalContext _context;

        public GarageStaffController(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.GarageStaffs
                .Include(gs => gs.User)
                .Include(gs => gs.Garage)
                .ToListAsync();

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.Users.Where(u => u.Role == "Technician"), "UserID", "FullName");
            ViewBag.Garages = new SelectList(_context.Garages, "GarageID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GarageStaff model)
        {
            _context.GarageStaffs.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
