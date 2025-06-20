using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetVehiclesByUserIdAsync(int userId);
        Task<Vehicle?> GetByIdAsync(int id);
        Task CreateAsync(Vehicle vehicle);
        Task<bool> UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(int id);
    }
}
