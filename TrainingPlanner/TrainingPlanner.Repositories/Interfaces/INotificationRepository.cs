using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> UpdateNotification(Notification notification);
        Task<Notification> CreateNotification(Notification notification);
        Task<Notification> GetNotification(string userId);
    }
}
