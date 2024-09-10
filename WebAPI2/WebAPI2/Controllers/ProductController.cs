using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI2.Data;
using WebAPI2.DTO;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ITIContext2 context;
        private readonly IProductRepo productRepo;

        public ProductController(ITIContext2 _context , IProductRepo _productRepo)
        {
            context = _context;
            productRepo = _productRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<Product> prodlist = await productRepo.GetProductsAsync();
            return Ok(prodlist);
        }

        [HttpGet("{id:int}")]   
        public async Task<ActionResult> GetById(int id) {
            Product prod = await productRepo.GetProductByIdAsync(id);
            GeneralResponse generalResponse = new GeneralResponse();    
            if (prod != null) {
                generalResponse.Success = true;
                generalResponse.Data = prod;
            }
            else
            {
                generalResponse.Success = false;
                generalResponse.Data = "Invaild ID";
            }
            return Ok(generalResponse);
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<ActionResult> Create(Product product){
            await productRepo.CreateAsync(product);
            return CreatedAtAction("GetById" , new {id = product.Id} , product);
        }

        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult> Edit(int id ,  Product product) {
            await productRepo.UpdateAsync(id, product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await productRepo.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchProducts(ProductDTO productFromReq)
        {
            var products = await productRepo.GetProductsAsync();

            if (!string.IsNullOrEmpty(productFromReq.keyword))
            {
                products = products.Where(p => p.Name.Contains(productFromReq.keyword , StringComparison.OrdinalIgnoreCase) 
                || p.Description.Contains(productFromReq.keyword , StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(productFromReq.category))
            {
                products = products.Where(p => p.Category.Name.Equals(productFromReq.category));
            }

            if (productFromReq.minPrice > 0)
            {
                products = products.Where(p => p.Price >= productFromReq.minPrice.Value);   
            }

            if(productFromReq.maxPrice > 0)
            {
                products = products.Where(p => p.Price <= productFromReq.maxPrice.Value);
            }

            return Ok(products);
        }
    }
}
