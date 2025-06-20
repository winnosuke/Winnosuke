using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface IGarageScheduleService
    {
        Task<List<GarageSchedule>> GetAllAsync();
        Task<List<GarageSchedule>> GetByGarageIdAsync(int garageId);
        Task<GarageSchedule?> GetByIdAsync(int id);
        Task CreateAsync(GarageSchedule schedule);
        Task<bool> UpdateAsync(GarageSchedule schedule);
        Task DeleteAsync(int id);
    }
}
