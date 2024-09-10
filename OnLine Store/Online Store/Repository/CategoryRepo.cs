using Microsoft.EntityFrameworkCore;
using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        ITIContext2 context;

        public CategoryRepo(ITIContext2 _context)
        {
            context = _context;
        }

        public async Task CreateAsync(Category category)
        {
            await context.Categorys.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await context.Categorys.SingleOrDefaultAsync(p => p.Id == id);
            context.Categorys.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            Category category = await context.Categorys.SingleOrDefaultAsync(x => x.Id == id);
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            IEnumerable<Category> categories = await context.Categorys.ToListAsync();
            return categories;
        }

        public async Task UpdateAsync(int Id, Category category)
        {
            Category categoryFromDB = await context.Categorys.SingleOrDefaultAsync(x => x.Id == Id);
            categoryFromDB.Name = category.Name;
            categoryFromDB.Products = category.Products;
            await context.SaveChangesAsync();
        }
    }
}
