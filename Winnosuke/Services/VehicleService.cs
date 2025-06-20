using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly MyGarageFinalContext _context;

        public VehicleService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetVehiclesByUserIdAsync(int userId)
        {
            return await _context.Vehicles
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task CreateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Vehicle vehicle)
        {
            var existing = await _context.Vehicles.FindAsync(vehicle.VehicleId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
