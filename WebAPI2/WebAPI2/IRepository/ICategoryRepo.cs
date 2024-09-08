using WebAPI2.Model;

namespace WebAPI2.IRepository
{
    public interface ICategoryRepo
    {
        public List<Category> GetCategory();
        public Category GetCategoryById(int id);
        public void Create(Category category);
        public void Update(int Id, Category category);
        public void Delete(int id);
    }
}
