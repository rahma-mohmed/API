using WebAPI2.Model;

namespace WebAPI2.IRepository
{
    public interface ICartRepo
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task CreateCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task RemoveCartItemAsync(string userId, int productId);
    }
}
