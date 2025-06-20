using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;
using Winnosuke.Services;
using Winnosuke.ViewModels;

namespace Winnosuke.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly MyGarageFinalContext _context;

        public RevenueService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<RevenueViewModel> GetRevenueAsync()
        {
            var completed = _context.Appointments
                .Where(a => a.Status == "Done" || a.Status == "Approved")
                .Include(a => a.Service)
                .Include(a => a.Garage);

            var monthly = await completed
    .GroupBy(a => a.AppointmentTime.Value.ToString("yyyy-MM"))
    .Select(g => new RevenueByMonth
    {
        Month = g.Key,
        Total = g.Sum(a => a.Service.Price ?? 0) // Explicitly handle nullable decimal
    }).ToListAsync();

            var byGarage = await completed
                .GroupBy(a => a.Garage.Name)
                .Select(g => new RevenueByGarage
                {
                    GarageName = g.Key,
                    Total = g.Sum(a => a.Service.Price ?? 0) // Explicitly handle nullable decimal
                }).ToListAsync();

            var byService = await completed
                .GroupBy(a => a.Service.ServiceName)
                .Select(g => new RevenueByService
                {
                    ServiceName = g.Key,
                    Total = g.Sum(a => a.Service.Price ?? 0) // Explicitly handle nullable decimal
                }).ToListAsync();

            return new RevenueViewModel
            {
                MonthlyRevenue = monthly,
                GarageRevenue = byGarage,
                ServiceRevenue = byService
            };
        }
    }
}
