using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI2.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public int ProductId { get; set; } 
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } //"Pending", "Shipped", "Completed", "Cancelled"
        public string Address { get; set; } 
        public DateTime DateTime { get; }

        public Order()
        {
            DateTime = DateTime.Now;
        }
    }
}
