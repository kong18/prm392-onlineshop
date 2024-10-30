using PRM392.OnlineStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task<string> GetUserNotifications(int userId);
        Task<string> GetUnreadNotificationCount(int userId);
        Task<string> MarkNotificationAsRead(int notificationId);
        Task<string> CreateNotification(int userId, string message);
        Task<string> DeleteNotificationById(int notificationId);
    }
}
