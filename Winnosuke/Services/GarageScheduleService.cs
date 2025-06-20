using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public class GarageScheduleService : IGarageScheduleService
    {
        private readonly MyGarageFinalContext _context;

        public GarageScheduleService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<List<GarageSchedule>> GetAllAsync()
        {
            return await _context.GarageSchedules
                .Include(s => s.Garage)
                .ToListAsync();
        }

        public async Task<List<GarageSchedule>> GetByGarageIdAsync(int garageId)
        {
            return await _context.GarageSchedules
                .Where(s => s.GarageId == garageId)
                .Include(s => s.Garage)
                .ToListAsync();
        }

        public async Task<GarageSchedule?> GetByIdAsync(int id)
        {
            return await _context.GarageSchedules
                .Include(s => s.Garage)
                .FirstOrDefaultAsync(s => s.ScheduleId == id);
        }

        public async Task CreateAsync(GarageSchedule schedule)
        {
            _context.GarageSchedules.Add(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(GarageSchedule schedule)
        {
            var existing = await _context.GarageSchedules.FindAsync(schedule.ScheduleId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var schedule = await _context.GarageSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.GarageSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
