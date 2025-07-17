using BloodDonationSupportSystem.DTOs.NotificationDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Implementations;
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

        public void EventCreateNotification(EventNotificationCreateDTO dto )
        {
            var notif = new Notification
            {
                Message = dto.Message,
                EventId = dto.EventId,
                UserId = null,
                NotifDate = DateOnly.FromDateTime(DateTime.Today)
            };

            _notificationRepo.CreateNotification(notif);
        }

        public void AdimnCreateNotification(AdminNotificationCreateDTO dto)
        {
            var notif = new Notification
            {
                Message = dto.Message,
                UserId = dto.UserId,
                NotifDate = DateOnly.FromDateTime(DateTime.Today),
                EventId = null 
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

        public NotificationUnreadCountDTO GetUnreadCount(int userId)
        {
            int count = _notificationRepo.CountUnreadByUserId(userId);
            return new NotificationUnreadCountDTO
            {
                RealCount = count
            };
        }

        public void MarkAsRead(int notificationId)
        {
            _notificationRepo.MarkAsRead(notificationId);
        }

        public void MarkAllAsRead(int userId)
        {
            _notificationRepo.MarkAllAsRead(userId); 
        }
    }

}
