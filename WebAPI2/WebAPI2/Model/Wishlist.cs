using System.Text.Json.Serialization;

namespace Online_Store_API.Model
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public virtual ICollection<WishlistItem>? WishlistItems { get; set; } = new List<WishlistItem>();
    }
}
