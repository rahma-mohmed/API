using Online_Store_API.Model;

namespace Online_Store_API.IRepository
{
    public interface IPaymentRepo
    {
        Task<Payment> ProcessPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(int id);
    }
}
