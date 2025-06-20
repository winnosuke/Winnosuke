using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public class TechnicalReportService : ITechnicalReportService
    {
        private readonly MyGarageFinalContext _context;

        public TechnicalReportService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<List<TechnicalReport>> GetByTechnicianIdAsync(int techId)
        {
            return await _context.TechnicalReports
                .Where(r => r.TechnicianId == techId)
                .Include(r => r.Appointment)
                .ToListAsync();
        }

        public async Task<List<TechnicalReport>> GetByUserIdAsync(int userId)
        {
            return await _context.TechnicalReports
                .Include(r => r.Appointment)
                .ThenInclude(a => a.User)
                .Where(r => r.Appointment.UserId == userId)
                .ToListAsync();
        }

        public async Task<TechnicalReport?> GetByIdAsync(int id)
        {
            return await _context.TechnicalReports
                .Include(r => r.Appointment)
                .FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task CreateAsync(TechnicalReport report)
        {
            _context.TechnicalReports.Add(report);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TechnicalReport report)
        {
            var existing = await _context.TechnicalReports.FindAsync(report.ReportId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
