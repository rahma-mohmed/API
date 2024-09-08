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

        public void Create(Category category)
        {
            context.Categorys.Add(category);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Category category = context.Categorys.SingleOrDefault(p => p.Id == id);
            context.Categorys.Remove(category);
            context.SaveChanges();
        }

        public Category GetCategoryById(int id)
        {
            Category category = context.Categorys.SingleOrDefault(x => x.Id == id);
            return category;
        }

        public List<Category> GetCategory()
        {
            List<Category> categories = context.Categorys.ToList();
            return categories;
        }

        public void Update(int Id, Category category)
        {
            Category categoryFromDB = context.Categorys.SingleOrDefault(x => x.Id == Id);
            categoryFromDB.Name = category.Name;
            categoryFromDB.Products = category.Products;
            context.SaveChanges();
        }
    }
}
