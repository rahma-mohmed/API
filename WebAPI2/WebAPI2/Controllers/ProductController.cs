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
        public IActionResult GetAll()
        {
            List<Product> prodlist =productRepo.GetProducts();
            return Ok(prodlist);
        }

        [HttpGet("{id:int}")]   
        public ActionResult GetById(int id) {
            Product prod = productRepo.GetProductById(id);
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
        public IActionResult Create(Product product){
            productRepo.Create(product);
            return CreatedAtAction("GetById" , new {id = product.Id} , product);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id ,  Product product) {
            productRepo.Update(id, product);
            return NoContent();
        }

        [HttpGet(@"Delete/{id}")]
        public IActionResult Delete(int id)
        {
            productRepo.Delete(id);
            return NoContent();
        }
    }
}
