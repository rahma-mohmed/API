using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product prod = await context.Products.SingleOrDefaultAsync(p => p.Id == id);
            context.Products.Remove(prod);
            await context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await context.Products.SingleOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            IEnumerable<Product> products = await context.Products.Include(p => p.Category).ToListAsync();
            return products;
        }

        public async Task UpdateAsync(int Id, Product product)
        {
            Product productFromDB = await context.Products.SingleOrDefaultAsync(x => x.Id == Id);
            productFromDB.Name = product.Name;
            productFromDB.Description = product.Description;
            productFromDB.Price = product.Price;
            productFromDB.Category = product.Category;
            await context.SaveChangesAsync();
        }
    }
}
