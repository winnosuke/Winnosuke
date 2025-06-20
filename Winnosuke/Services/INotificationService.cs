using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetByUserIdAsync(int userId);
        Task MarkAsReadAsync(int id);
        Task<Notification?> GetByIdAsync(int id);
    }
}
