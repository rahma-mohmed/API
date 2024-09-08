using WebAPI2.Model;

namespace WebAPI2.IRepository
{
    public interface IProductRepo
    {
        public List<Product> GetProducts();
        public Product GetProductById(int id);
        public void Create(Product product);
        public void Update(int Id , Product product);
        public void Delete(int id);
    }
}
