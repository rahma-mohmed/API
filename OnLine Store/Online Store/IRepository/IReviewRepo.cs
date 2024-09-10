using WebAPI2.Model;

namespace WebAPI2.IRepository
{
    public interface IReviewRepo
    {
        public Task<IEnumerable<Review>> GetAllReviewsAsync();
        public Task<Review> GetReviewByIdAsync(int id);
        public Task AddReviewAsync(Review review);
        public Task UpdateReviewAsync(Review review);
        public Task DeleteReviewAsync(int id);
        public Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
    }
}
