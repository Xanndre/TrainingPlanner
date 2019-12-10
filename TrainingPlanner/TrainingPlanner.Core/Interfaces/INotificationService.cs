using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Notification;

namespace TrainingPlanner.Core.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDTO> UpdateNotification(NotificationDTO notification);
        Task<NotificationDTO> GetNotification(string userId);
    }
}
