using Winnosuke.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Winnosuke.Services
{
    public interface IGarageService
    {
        Task<IEnumerable<Garage>> GetAllAsync();
        Task<Garage?> GetByIdAsync(int id);
        Task AddAsync(Garage garage);
        Task UpdateAsync(Garage garage);
        Task DeleteAsync(int id);
    }
}
