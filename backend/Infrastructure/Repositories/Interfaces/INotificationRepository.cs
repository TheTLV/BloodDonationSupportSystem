using System;

public interface INotificationRepository
{
    void AddNotification(string message);
    void RemoveNotification(Guid notificationId);
    void ClearNotifications();
}

