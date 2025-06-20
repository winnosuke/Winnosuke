using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface IRepairStatusService
    {
        Task<IEnumerable<RepairStatus>> GetByAppointmentIdAsync(int appointmentId);
        Task<RepairStatus?> GetByIdAsync(int id);
        Task AddAsync(RepairStatus status);
        Task UpdateAsync(RepairStatus status);
        Task DeleteAsync(int id);
    }
}
