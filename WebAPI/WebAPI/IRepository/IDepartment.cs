using WebAPI.Models;

namespace WebAPI.IRepository
{
    public interface IDepartment
    {
        public Department GetById(int id);  
        public List<Department> GetAll();
        public void Create(Department department);
        public void Update(int ID, Department department);
        public void Delete(int id);
    }
}
