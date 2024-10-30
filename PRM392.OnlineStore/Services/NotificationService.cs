using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Application.Common.Interfaces;
using System.Globalization;
using Newtonsoft.Json;

namespace PRM392.OnlineStore.Api.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<string> GetUserNotifications(int userId)
        {
            try
            {
                var notifications = await _notificationRepository.GetUserNotifications(userId);

                if (notifications == null || notifications.Count == 0)
                {
                    return JsonConvert.SerializeObject(new { message = "No notifications found for this user ID." });
                }

                var result = notifications.Select(n => new
                {
                    n.NotificationId,
                    n.UserId,
                    n.Message,
                    n.IsRead,
                    CreatedAt = n.CreatedAt.ToString("HH:mm dd/MM/yyyy")
                });

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { message = "An error occurred while retrieving notifications." });
            }
        }

        public async Task<string> GetUnreadNotificationCount(int userId)
        {
            var count = await _notificationRepository.GetUnreadNotificationCount(userId);
            return JsonConvert.SerializeObject(new { userId, unreadCount = count });
        }

        public async Task<string> MarkNotificationAsRead(int notificationId)
        {
            var notification = await _notificationRepository.FindAsync(n => n.NotificationId == notificationId);

            if (notification == null)
            {
                return JsonConvert.SerializeObject(new { message = "Invalid Notification ID. Please check and try again." });
            }

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                await _notificationRepository.UnitOfWork.SaveChangesAsync();
            }

            return JsonConvert.SerializeObject(new { message = "Notification marked as read." });
        }

        public async Task<string> CreateNotification(int userId, string message)
        {
            if (userId <= 0)
            {
                return JsonConvert.SerializeObject(new { message = "User ID is missing or invalid." });
            }

            var notification = new Notification
            {
                UserId = userId,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepository.AddNotification(notification);

            return JsonConvert.SerializeObject(new
            {
                notification.UserId,
                notification.Message,
                notification.IsRead,
                CreatedAt = notification.CreatedAt.ToString("HH:mm dd/MM/yyyy", CultureInfo.InvariantCulture)
            });
        }

        public async Task<string> DeleteNotificationById(int notificationId)
        {
            var notification = await _notificationRepository.FindAsync(n => n.NotificationId == notificationId);
            if (notification == null)
            {
                return JsonConvert.SerializeObject(new { message = "Notification ID does not exist. Please verify." });
            }

            await _notificationRepository.RemoveNotification(notification);
            return JsonConvert.SerializeObject(new { message = "Notification deleted successfully." });
        }
    }
}
