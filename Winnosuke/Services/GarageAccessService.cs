using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public class GarageAccessService : IGarageAccessService
    {
        private readonly MyGarageFinalContext _context;

        public GarageAccessService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetGarageIdsForUserAsync(int userId)
        {
            return await _context.GarageStaffs
                .Where(gs => gs.UserId == userId)
                .Select(gs => gs.GarageId)
                .ToListAsync();
        }
    }

}
