using WebAPI2.Model;

namespace WebAPI2.IRepository
{
    public interface ICategoryRepo
    {
        public Task<IEnumerable<Category>> GetCategoryAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task CreateAsync(Category category);
        public Task UpdateAsync(int Id, Category category);
        public Task DeleteAsync(int id);
    }
}
