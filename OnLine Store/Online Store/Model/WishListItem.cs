using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebAPI2.Model;

namespace Online_Store_API.Model
{
    public class WishlistItem
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [JsonIgnore]
        [ForeignKey("Wishlist")]
        public int? WishlistId { get; set; }
        public Wishlist? Wishlist { get; set; }
    }
}
