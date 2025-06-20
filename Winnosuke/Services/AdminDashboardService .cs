using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;
using Winnosuke.ViewModels;

namespace Winnosuke.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly MyGarageFinalContext _context;

        public AdminDashboardService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var totalUsers = await _context.Users.CountAsync(u => u.Role == "Customer" || u.Role == "Technician");
            var totalAppointments = await _context.Appointments.CountAsync();
            var totalRevenue = await _context.Appointments
     .Where(a => a.Status == "Approved" || a.Status == "Done")
     .Join(_context.Services,
         a => a.ServiceId,
         s => s.ServiceId,
         (a, s) => s.Price)
     .SumAsync() ?? 0m;

            return new DashboardViewModel
            {
                TotalRevenue = totalRevenue,
                // Other properties remain unchanged
            };
            var totalReports = await _context.TechnicalReports.CountAsync();

            var topRated = await _context.Reviews
                .GroupBy(r => r.GarageId)
                .Select(g => new GarageRatingViewModel
                {
                    GarageName = g.First().Garage.Name,
                    AverageRating = g.Average(r => r.Rating) ?? 0.0,
                    ReviewCount = g.Count()
                })
                .OrderByDescending(g => g.AverageRating)
                .ThenByDescending(g => g.ReviewCount)
                .Take(3)
                .ToListAsync();

            return new DashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalAppointments = totalAppointments,
                TotalRevenue = totalRevenue,
                TotalReports = totalReports,
                TopRatedGarages = topRated
            };
        }
    }
}
