using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Store_API.IRepository;
using Online_Store_API.Model;
using WebAPI2.Data;
using WebAPI2.Model;

namespace Online_Store_API.Repository
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly ITIContext2 _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public NotificationRepo(ITIContext2 context , IHttpContextAccessor httpContextAccessor , UserManager<User> userManagrt)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManagrt;
        }

        public async Task<List<Notification>> GetUserNotificationsAsync()
        {
            User user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return await _context.Notifications.Where(n => n.UserId == user.Id).
                OrderByDescending(n => n.DateSent)
                .ToListAsync();
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
    }
}
