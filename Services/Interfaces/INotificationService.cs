using BloodDonationSupportSystem.DTOs.NotificationDTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface INotificationService
    {
        void CreateNotification(NotificationCreateDTO dto, int? userId =null);
        List<NotificationViewDTO> GetAllNotifications();
        List<NotificationViewDTO> GetNotificationsByUser(int userId);
        bool DeleteNotification(int id);
    }

}
