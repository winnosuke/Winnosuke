
using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface ITechnicalReportService
    {
        Task<List<TechnicalReport>> GetByTechnicianIdAsync(int techId);
        Task<List<TechnicalReport>> GetByUserIdAsync(int userId);
        Task<TechnicalReport?> GetByIdAsync(int id);
        Task CreateAsync(TechnicalReport report);
        Task<bool> UpdateAsync(TechnicalReport report);
    }
}
