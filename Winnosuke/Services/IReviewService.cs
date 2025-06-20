using Winnosuke.Models;

namespace Winnosuke.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync();
        Task<List<Review>> GetByUserIdAsync(int userId);
        Task<Review?> GetByIdAsync(int id);
        Task CreateAsync(Review review);
        Task DeleteAsync(int id);
    }
}
