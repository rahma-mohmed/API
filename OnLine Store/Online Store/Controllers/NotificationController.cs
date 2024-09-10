using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Store_API.IRepository;
using Online_Store_API.Model;

namespace Online_Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepo _notificationRepo;

        public NotificationController(INotificationRepo notificationRepository)
        {
            _notificationRepo = notificationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNotifications()
        {
            var notifications = await _notificationRepo.GetUserNotificationsAsync();
            return Ok(notifications);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(Notification notification)
        {
            await _notificationRepo.SendNotificationAsync(notification);
            return Ok("Notification sent");
        }
    }
}
