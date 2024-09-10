using WebAPI2.Model;

namespace WebAPI2.IRepository
{
    public interface IProductRepo
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<Product> GetProductByIdAsync(int id);
        public Task CreateAsync(Product product);
        public Task UpdateAsync(int Id , Product product);
        public Task DeleteAsync(int id);
    }
}
