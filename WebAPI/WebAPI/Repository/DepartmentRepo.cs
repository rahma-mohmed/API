using WebAPI.IRepository;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class DepartmentRepo : IDepartment
    {
        ITIContext context;

        public DepartmentRepo(ITIContext _context)
        {
            context = _context;
        }

        public void Create(Department department)
        {
            context.Add(department);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Department? dept = context.Department.SingleOrDefault(d => d.Id == id);
            if (dept != null) {
                context.Department.Remove(dept);
                context.SaveChanges();
            }
        }

        public List<Department> GetAll()
        {
            List<Department> deptList = context.Department.ToList();
            return deptList;
        }

        public Department GetById(int id)
        {
            Department dept = context.Department.SingleOrDefault(de => de.Id == id);
            return dept;
        }

        public void Update(int ID ,Department department)
        {
            Department dept = context.Department.SingleOrDefault(x => x.Id == ID);
            if (dept != null) {
                dept.Name = department.Name;
                dept.ManagerName = department.ManagerName;
            }
            context.SaveChanges();
        }
    }
}
