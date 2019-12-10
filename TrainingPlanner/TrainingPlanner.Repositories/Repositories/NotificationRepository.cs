using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TrainingPlanner.Data;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Repositories.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(TrainingPlannerDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Notification> UpdateNotification(Notification notification)
        {
            _trainingPlannerDbContext.Update(notification);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> CreateNotification(Notification notification)
        {
            await _trainingPlannerDbContext.Notifications.AddAsync(notification);
            await _trainingPlannerDbContext.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> GetNotification(string userId)
        {
            return await GetNotificationQuery()
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        private IQueryable<Notification> GetNotificationQuery()
        {
            return _trainingPlannerDbContext.Notifications;
        }
    }
}
