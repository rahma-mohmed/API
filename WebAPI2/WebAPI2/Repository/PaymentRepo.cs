using Microsoft.AspNetCore.Identity;
using Online_Store_API.IRepository;
using Online_Store_API.Model;
using WebAPI2.Data;
using WebAPI2.Model;

namespace Online_Store_API.Repository
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly ITIContext2 _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public PaymentRepo(ITIContext2 context , IHttpContextAccessor contextAccessor , UserManager<User> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<Payment> ProcessPaymentAsync(Payment payment)
        {
            User user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            payment.UserId = user.Id;
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
