using Microsoft.EntityFrameworkCore;
using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Repository
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ITIContext2 _context;

        public ReviewRepo(ITIContext2 context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null) {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.Reviews.Where(r => r.ProductId == productId).ToListAsync();
        }

        public async Task UpdateReviewAsync(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;    
            await _context.SaveChangesAsync();
        }
    }
}
