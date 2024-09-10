using System.ComponentModel.DataAnnotations;

namespace WebAPI2.Model
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        public int ProductId { get; set; } 
        public string Content { get; set; }
        [Range(1,5)]
        public int Rating { get; set; } // Rating value (e.g., 1-5)
        public DateTime ReviewDate { get; set; }

        public Review()
        {
            ReviewDate = DateTime.Now; 
        }
    }
}
