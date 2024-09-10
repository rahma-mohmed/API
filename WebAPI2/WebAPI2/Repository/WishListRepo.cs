using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Store_API.DTO;
using Online_Store_API.IRepository;
using Online_Store_API.Model;
using WebAPI2.Data;
using WebAPI2.Model;

namespace Online_Store_API.Repository
{
    public class WishListRepo : IWishlistRepo
    {
        private readonly ITIContext2 _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _usermanager;

        public WishListRepo(ITIContext2 context , IHttpContextAccessor contextAccessor , UserManager<User> usermanager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _usermanager = usermanager;
        }

        public async Task AddToWhishlistAsync(int productId)
        {
            User? user = await _usermanager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }
            Wishlist whishlist = await _context.Wishlist.Include(w => w.WishlistItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(u => u.UserId == user.Id);

            if(whishlist == null)
            {
                whishlist = new Wishlist { UserId = user.Id};
                _context.Wishlist.Add(whishlist);
                await _context.SaveChangesAsync();

                whishlist.WishlistItems.Add(new WishlistItem { ProductId = productId });
                _context.Wishlist.Update(whishlist);

                await _context.SaveChangesAsync();
            }
            else
            {
                whishlist.WishlistItems.Add(new WishlistItem { ProductId = productId});
                _context.Wishlist.Update(whishlist);

                await _context.SaveChangesAsync();
            }
        }

    /*  public async Task<Wishlist> GetWishlistByUserIdAsync()
        {
            User? user = await _usermanager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }
            else
            {
                return await _context.Wishlist.Include(w => w.WishlistItems)
                         .ThenInclude(i => i.Product)
                         .FirstOrDefaultAsync(w => w.UserId == user.Id);
            }
        }*/

        public async Task RemoveFromWishlistAsync(int productId)
        {
            User? user = await _usermanager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            Wishlist whishlis = await _context.Wishlist.Include(w => w.WishlistItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(u => u.UserId == user.Id);

            if (whishlis == null) {  throw new Exception("No Whishlist!!"); }

            var item = whishlis?.WishlistItems?.FirstOrDefault(w => w.ProductId == productId);

            if(item != null)
            {
                whishlis?.WishlistItems?.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No Such Product in Whishlist!!");
            }
        }

        public async Task<WishListDTO> GetWishlistDTOByUserIdAsync()
        {
            User? user = await _usermanager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }
            else
            {
                var res =  await _context.Wishlist.Include(w => w.WishlistItems)
                         .ThenInclude(i => i.Product)
                         .FirstOrDefaultAsync(w => w.UserId == user.Id);

                if (res != null) {
                    List<Product> products = new List<Product>();

                    foreach (var item in res.WishlistItems)
                    {
                        products.Add(item.Product);
                    }

                    WishListDTO wishList = new WishListDTO()
                    {
                        Products = products,
                        UserId = user.Id,
                        whishlistId = res.Id
                    };

                    return wishList;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
