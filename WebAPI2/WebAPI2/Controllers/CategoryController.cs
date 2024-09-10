using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;
using WebAPI2.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Category> categorylist = await categoryRepo.GetCategoryAsync();
            return Ok(categorylist);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            Category category = await categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await categoryRepo.CreateAsync(category);
            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            await categoryRepo.UpdateAsync(id, category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryRepo.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await categoryRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
