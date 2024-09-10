using Online_Store_API.DTO;
using Online_Store_API.Model;

namespace Online_Store_API.IRepository
{
    public interface IWishlistRepo
    {
        public Task AddToWhishlistAsync(int productId);
        public Task RemoveFromWishlistAsync(int productId);
        public Task<WishListDTO> GetWishlistDTOByUserIdAsync();
    }
}
