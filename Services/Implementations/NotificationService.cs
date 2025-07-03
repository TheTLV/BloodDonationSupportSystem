using BloodDonationSupportSystem.DTOs.NotificationDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;


        public NotificationService(INotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public void CreateNotification(NotificationCreateDTO dto , int? userId=null)
        {
            var notif = new Notification
            {
                Message = dto.Message,
                EventId = dto.EventId,
                UserId = userId,
                NotifDate = DateOnly.FromDateTime(DateTime.Today)
            };

            _notificationRepo.CreateNotification(notif);
        }

        public bool DeleteNotification(int id)
        {
            
            var ev = _notificationRepo.GetById(id);
            if (ev == null) return false;

            var success = _notificationRepo.DeleteNotification(id);
            return success;
        }
        

        public List<NotificationViewDTO> GetAllNotifications()
        {
            return _notificationRepo.GetAllNotifications()
                .Select(n => new NotificationViewDTO
                {
                    NotificationId = n.NotificationId,
                    Message = n.Message,
                    NotifDate = n.NotifDate,
                    EventId = n.EventId,
                    UserId = n.UserId
                })
                .ToList();
        }

        public List<NotificationViewDTO> GetNotificationsByUser(int userId)
        {
            return _notificationRepo.GetNotificationsByUserId(userId)
                .Select(n => new NotificationViewDTO
                {
                    NotificationId = n.NotificationId,
                    Message = n.Message,
                    NotifDate = n.NotifDate,
                    EventId = n.EventId,
                    UserId = n.UserId
                })
                .ToList();
        }
    }

}
