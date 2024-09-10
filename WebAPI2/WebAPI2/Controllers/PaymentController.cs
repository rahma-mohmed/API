using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Store_API.IRepository;
using Online_Store_API.Model;

namespace Online_Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepo _paymentRepository;

        public PaymentController(IPaymentRepo paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [Authorize]
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment(Payment payment)
        {
            var processedPayment = await _paymentRepository.ProcessPaymentAsync(payment);
            return Ok(processedPayment);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound("Payment not found");
            return Ok(payment);
        }

    }
}
