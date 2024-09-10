using WebAPI2.Model;

namespace Online_Store_API.DTO
{
    public class WishListDTO
    {
        public string UserId { get; set; }
        public int whishlistId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
