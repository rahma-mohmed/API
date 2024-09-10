using WebAPI2.Data;
using WebAPI2.Model;

namespace WebAPI2.IRepository
{
    public interface IOrderRepo
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task<Order> GetOrderByIdAsync(int id);
        public Task AddOrderAsync(Order order);
        public Task UpdateOrderAsync(int Id , Order order);
        public Task DeleteOrderAsync(int id);
    }
}
