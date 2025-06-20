using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface IAdminActivityService
    {
        Task<IEnumerable<AdminActivity>> GetAllAsync();
        Task<AdminActivity?> GetByIdAsync(int id);
        Task AddAsync(AdminActivity activity);
        Task DeleteAsync(int id);
    }
}
