using Online_Store_API.Model;

namespace Online_Store_API.IRepository
{
    public interface INotificationRepo
    {
        public Task<List<Notification>> GetUserNotificationsAsync();
        public Task SendNotificationAsync(Notification notification);
    }
}
