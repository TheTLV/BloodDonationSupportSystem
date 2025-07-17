using BloodDonationSupportSystem.DTOs.NotificationDTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface INotificationService
    {
        void EventCreateNotification(EventNotificationCreateDTO dto);
        void AdimnCreateNotification(AdminNotificationCreateDTO dto);
        List<NotificationViewDTO> GetAllNotifications();
        List<NotificationViewDTO> GetNotificationsByUser(int userId);
        bool DeleteNotification(int id);
        NotificationUnreadCountDTO GetUnreadCount(int userId);
        void MarkAsRead(int notificationId);
        void MarkAllAsRead(int userId);
    }

}
