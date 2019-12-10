using AutoMapper;
using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.Notification;
using TrainingPlanner.Core.Interfaces;
using TrainingPlanner.Data.Entities;
using TrainingPlanner.Repositories.Interfaces;

namespace TrainingPlanner.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<NotificationDTO> UpdateNotification(NotificationDTO notification)
        {
            var mappedNotification = _mapper.Map<Notification>(notification);
            var returnedNotification = await _notificationRepository.UpdateNotification(mappedNotification);
            return _mapper.Map<NotificationDTO>(returnedNotification);
        }

        public async Task<NotificationDTO> GetNotification(string userId)
        {
            var notification = await _notificationRepository.GetNotification(userId);
            return _mapper.Map<NotificationDTO>(notification);
        }
    }
}
