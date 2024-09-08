using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Repository
{
    public class ProductRepo : IProductRepo
    {
        ITIContext2 context;

        public ProductRepo(ITIContext2 _context)
        {
            context = _context;
        }

        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Product prod = context.Products.SingleOrDefault(p => p.Id == id);
            context.Products.Remove(prod);
            context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            Product product = context.Products.SingleOrDefault(x => x.Id == id);
            return product;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = context.Products.ToList();
            return products;
        }

        public void Update(int Id, Product product)
        {
            Product productFromDB = context.Products.SingleOrDefault(x => x.Id == Id);
            productFromDB.Name = product.Name;
            productFromDB.Description = product.Description;
            productFromDB.Price = product.Price;
            productFromDB.Category = product.Category;
            context.SaveChanges();
        }
    }
}
