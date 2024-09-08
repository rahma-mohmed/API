using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;
using WebAPI2.DTO;
using Microsoft.EntityFrameworkCore;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ITIContext2 context;
        private readonly ICategoryRepo categoryRepo;

        public CategoryController(ITIContext2 _context, ICategoryRepo _categoryRepo)
        {
            context = _context;
            categoryRepo = _categoryRepo;
        }

        [HttpGet("Count")]
        public ActionResult<List<CategoryWithProdDTO>> Get()
        {
            List<CategoryWithProdDTO> result = new List<CategoryWithProdDTO>();
            List<Category> categories = context.Categorys.Include(c => c.Products).ToList();

            foreach (var category in categories) {
                CategoryWithProdDTO categoryWithProdDTO = new CategoryWithProdDTO();
                categoryWithProdDTO.Name = category.Name;
                categoryWithProdDTO.Id = category.Id;
                categoryWithProdDTO.ProductCount = category.Products.Count();
                result.Add(categoryWithProdDTO);
            }
            return result;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categorylist = categoryRepo.GetCategory();
            return Ok(categorylist);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetById(int id)
        {
            Category category = categoryRepo.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            categoryRepo.Create(category);
            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Category category)
        {
            categoryRepo.Update(id, category);
            return NoContent();
        }

        [HttpGet(@"Delete/{id}")]
        public IActionResult Delete(int id)
        {
            categoryRepo.Delete(id);
            return NoContent();
        }
    }
}
