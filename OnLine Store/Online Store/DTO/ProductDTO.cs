namespace WebAPI2.DTO
{
    public class ProductDTO
    {
        public string keyword { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public string category { get; set; }
    }
}
