using PRM392.OnlineStore.Domain.Entities.Models;

namespace PRM392.OnlineStore.Domain.Entities.Repositories
{
    public interface INotificationRepository : IEFRepository<Notification, Notification>
    {
        Task<List<Notification>> GetUserNotifications(int userId);
        Task<int> GetUnreadNotificationCount(int userId);
        Task MarkNotificationAsRead(int notificationId);
        Task<List<Notification>> GetOldNotifications(int daysOld);
        Task AddNotification(Notification notification);
        Task RemoveNotification(Notification notification);
    }
}
