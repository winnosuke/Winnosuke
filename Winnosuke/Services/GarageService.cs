using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public class GarageService : IGarageService
    {
        private readonly MyGarageFinalContext _context;

        public GarageService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Garage>> GetAllAsync() =>
            await _context.Garages.ToListAsync();

        public async Task<Garage?> GetByIdAsync(int id) =>
            await _context.Garages.FindAsync(id);

        public async Task AddAsync(Garage garage)
        {
            _context.Garages.Add(garage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Garage garage)
        {
            _context.Garages.Update(garage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var garage = await _context.Garages.FindAsync(id);
            if (garage != null)
            {
                _context.Garages.Remove(garage);
                await _context.SaveChangesAsync();
            }
        }
    }
}