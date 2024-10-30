using AutoMapper;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Infrastructure.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification, Notification, ApplicationDbContext>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper) { }

        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await FindAllAsync(n => n.UserId == userId);
        }

        public async Task<int> GetUnreadNotificationCount(int userId)
        {
            return await CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task MarkNotificationAsRead(int notificationId)
        {
            var notification = await FindAsync(n => n.NotificationId == notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                Update(notification);
                await SaveChangesAsync();
            }
        }

        public async Task<List<Notification>> GetOldNotifications(int daysOld)
        {
            var cutoffDate = DateTime.UtcNow.AddDays(-daysOld);
            return await FindAllAsync(n => n.CreatedAt <= cutoffDate);
        }

        public async Task AddNotification(Notification notification)
        {
            Add(notification);
            await SaveChangesAsync();
        }

        public async Task RemoveNotification(Notification notification)
        {
            Remove(notification);
            await SaveChangesAsync();
        }
    }
}
