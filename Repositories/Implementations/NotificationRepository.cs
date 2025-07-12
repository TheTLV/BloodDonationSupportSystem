using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public List<Notification> GetAllNotifications()
        {
            return _context.Notifications
                .OrderByDescending(n => n.NotifDate)
                .ToList();
        }

        public List<Notification> GetNotificationsByUserId(int userId)
        {
            return _context.Notifications
                .Where(n => n.UserId == userId || n.UserId == null)
                .OrderByDescending(n => n.NotifDate)
                .ToList();
        }

        public bool DeleteNotification(int id)
        {
            var noti = _context.Notifications.Find(id);
            if (noti == null) return false;

            _context.Notifications.Remove(noti);
            return _context.SaveChanges() > 0;
        }
        public Notification? GetById(int id)
        {
            return _context.Notifications.FirstOrDefault(e => e.NotificationId == id);
        }
    }
}
