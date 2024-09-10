using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI2.Data;
using WebAPI2.IRepository;
using WebAPI2.Model;

namespace WebAPI2.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ITIContext2 _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderRepo(ITIContext2 context , UserManager<User> usermanager, IHttpContextAccessor httpContextAccessor)
        {
              _context = context;
            _userManager = usermanager;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task AddOrderAsync(Order order)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            order.UserId = user.Id;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null) {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task UpdateOrderAsync(int Id , Order order)
        {
            /*Order orderFromDB = await _context.Orders.FindAsync(Id);
            if (order != null) {
                orderFromDB.OrderDate = order.OrderDate;
                orderFromDB.Quantity = order.Quantity;
                orderFromDB.Status = order.Status;
                orderFromDB.TotalPrice = order.TotalPrice;
            }*/
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();  
        }
    }
}
