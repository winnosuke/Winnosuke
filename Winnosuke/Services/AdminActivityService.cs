using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public class AdminActivityService : IAdminActivityService
    {
        private readonly MyGarageFinalContext _context;

        public AdminActivityService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdminActivity>> GetAllAsync() =>
            await _context.AdminActivities.ToListAsync();

        public async Task<AdminActivity?> GetByIdAsync(int id) =>
            await _context.AdminActivities.FindAsync(id);

        public async Task AddAsync(AdminActivity activity)
        {
            _context.AdminActivities.Add(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var activity = await _context.AdminActivities.FindAsync(id);
            if (activity != null)
            {
                _context.AdminActivities.Remove(activity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
