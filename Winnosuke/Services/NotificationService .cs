using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Services
{
    public class NotificationService : INotificationService
    {
        private readonly MyGarageFinalContext _context;

        public NotificationService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task MarkAsReadAsync(int id)
        {
            var noti = await _context.Notifications.FindAsync(id);
            if (noti != null && noti.IsRead == false)
            {
                noti.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
