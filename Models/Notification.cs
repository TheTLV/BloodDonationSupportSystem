namespace BloodDonationSupportSystem.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
        public int? EventId { get; set; }
        public Event Event { get; set; }
    }
}