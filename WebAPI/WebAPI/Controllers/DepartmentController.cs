using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IRepository;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ITIContext context;
        private readonly IDepartment deptRepo;

        public DepartmentController(ITIContext _context , IDepartment _dept)
        {
            context = _context;
            deptRepo = _dept;
        }

        
        [HttpGet]
        [Authorize]
        public IActionResult GetAllDept()
        {
            List<Department> DeptList = deptRepo.GetAll();
            return Ok(DeptList);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetDept(int id)
        {
            Department dept = deptRepo.GetById(id);
            return Ok(dept);
        }

        [HttpPost]
        public IActionResult AddDept(Department dept)
        {
            deptRepo.Create(dept);
            //return Created($"http://localhost:44314/api/Department/{dept.Id}",dept);
            return CreatedAtAction("GetDept", new { id = dept.Id }, dept);
        }

        [HttpPut("{ID:int}")]
        public IActionResult UpdateDept(int ID,Department department)
        {
            deptRepo.Update(ID,department);
            context.SaveChanges();
            return NoContent();
        }

        [HttpGet(@"Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            deptRepo.Delete(Id);
            return NoContent();
        }

        /*[HttpPost]
        public IActionResult TestObj(Department dept , string name)
        {
            return Ok();
        }*/

        [HttpGet("{Id:int}/{Name}/{ManagerName}")]
        public IActionResult TestObj2([FromRoute]Department dept)
        {
            return Ok(dept);
        }
    }
}
