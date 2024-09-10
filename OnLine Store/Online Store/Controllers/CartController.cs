using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI2.DTO;
using WebAPI2.IRepository;
using WebAPI2.Model;
using WebAPI2.Repository;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _usermanager;
        private readonly IProductRepo _productRepo;
        private readonly ICartRepo _cartRepo;

        public CartController(IHttpContextAccessor httpContextAccessor , UserManager<User> userManager
            , IProductRepo productRepo,ICartRepo cartRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _usermanager = userManager;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        [HttpPost("add-item")]
        [Authorize]
        public async Task<IActionResult> AddToCart(CartItemDTO cartItemDTO)
        {
            var Product = await _productRepo.GetProductByIdAsync(cartItemDTO.ProductId); 
            
            if(Product == null)
            {
                return NotFound("Product not found");
            }

            User user = await _usermanager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var cart = await _cartRepo.GetCartByUserIdAsync(user.Id); 

            if(cart == null)
            {
                cart = new Cart()
                {
                    UserId = user.Id
                };
                await _cartRepo.CreateCartAsync(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == Product.Id);

            if(existingItem != null)
            {
                existingItem.Quantity += cartItemDTO.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = Product.Id,
                    Product = Product,
                    Quantity = cartItemDTO.Quantity
                });
            }

            await _cartRepo.UpdateCartAsync(cart);
            return Ok("Item added to cart");
        }

        [HttpDelete("remove-item/{productId}")]
        [Authorize]
        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {
            User user = await _usermanager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var cart = await _cartRepo.GetCartByUserIdAsync(user.Id);

            if(cart == null)
            {
                return NotFound("Cart not found");
            }

            await _cartRepo.RemoveCartItemAsync(user.Id,productId);
            return Ok("Item removed from cart");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            User user = await _usermanager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var cart = await _cartRepo.GetCartByUserIdAsync(user.Id);
            if (cart == null) return NotFound("Cart not found");
            return Ok(cart);
        }
    }
}
