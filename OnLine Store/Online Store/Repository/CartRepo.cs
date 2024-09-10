using Microsoft.EntityFrameworkCore;
using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Repository
{
    public class CartRepo : ICartRepo
    {
        public readonly ITIContext2 _context;

        public CartRepo(ITIContext2 context)
        {
            _context = context;
        }

        public async Task CreateCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts.Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task RemoveCartItemAsync(string userId, int productId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) return;

            var items = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if(items != null)
            {
                cart.Items.Remove(items);
                await UpdateCartAsync(cart);
            }
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
