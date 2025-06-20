using Microsoft.EntityFrameworkCore; // Add this using directive
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public class RepairStatusService : IRepairStatusService
    {
        private readonly MyGarageFinalContext _context;

        public RepairStatusService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RepairStatus>> GetByAppointmentIdAsync(int appointmentId) =>
            await _context.RepairStatuses.Where(r => r.AppointmentId == appointmentId).ToListAsync(); // Corrected property name

        public async Task<RepairStatus?> GetByIdAsync(int id) =>
            await _context.RepairStatuses.FindAsync(id); // Corrected property name

        public async Task AddAsync(RepairStatus status)
        {
            _context.RepairStatuses.Add(status); // Corrected property name
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RepairStatus status)
        {
            _context.RepairStatuses.Update(status); // Corrected property name
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var status = await _context.RepairStatuses.FindAsync(id); // Corrected property name
            if (status != null)
            {
                _context.RepairStatuses.Remove(status); // Corrected property name
                await _context.SaveChangesAsync();
            }
        }
    }
}