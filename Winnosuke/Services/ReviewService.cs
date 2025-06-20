using Microsoft.EntityFrameworkCore;
using Winnosuke.Models;
using Winnosuke.Services;

namespace Winnosuke.Services
{
    public class ReviewService : IReviewService
    {
        private readonly MyGarageFinalContext _context;

        public ReviewService(MyGarageFinalContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Garage)
                .ToListAsync();
        }

        public async Task<List<Review>> GetByUserIdAsync(int userId)
        {
            return await _context.Reviews
                .Where(r => r.UserId == userId)
                .Include(r => r.Garage)
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Garage)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }

        public async Task CreateAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
