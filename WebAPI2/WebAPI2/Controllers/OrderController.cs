using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ITIContext2 _context;
        private readonly IOrderRepo _orderRepo;

        public OrderController(ITIContext2 context, IOrderRepo orderRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostOrder(Order order)
        {

            await _orderRepo.AddOrderAsync(order);
            return CreatedAtAction("GetOrderById", new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            try
            {
                await _orderRepo.UpdateOrderAsync(id, order);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if(order == null)
            {
                return NotFound();
            }

            await _orderRepo.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
