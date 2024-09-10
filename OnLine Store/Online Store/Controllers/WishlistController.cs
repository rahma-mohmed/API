using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Store_API.IRepository;

namespace Online_Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepo _wishlistRepo;

        public WishlistController(IWishlistRepo wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetWishlist()
        {
            var wishlist = await _wishlistRepo.GetWishlistDTOByUserIdAsync();
            if (wishlist == null) {
                return NotFound("wishlist Is Empty, Add items and Try again");
            }
            else
            {
                return Ok(wishlist);
            }
        }

        [HttpPost("add/{productId}")]
        [Authorize]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            await _wishlistRepo.AddToWhishlistAsync(productId);
            return Ok("Product added to wishlist successfully");
        }

        [Authorize]
        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            await _wishlistRepo.RemoveFromWishlistAsync(productId);
            return Ok("Product removed from wishlist");
        }
    }
}
