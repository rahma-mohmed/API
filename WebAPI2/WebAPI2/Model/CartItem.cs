using Online_Store_API.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI2.Model
{
    public class CartItem
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
