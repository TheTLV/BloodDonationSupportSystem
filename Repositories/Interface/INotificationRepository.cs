﻿using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface INotificationRepository
    {
        void CreateNotification(Notification notification);
        List<Notification> GetAllNotifications();
        List<Notification> GetNotificationsByUserId(int userId);
        bool DeleteNotification(int id);
        Notification? GetById(int id);
    }

}
