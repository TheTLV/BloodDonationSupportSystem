namespace BloodDonationSupportSystem.DTOs.NotificationDTOs
{
    public class NotificationViewDTO
    {
        public int NotificationId { get; set; }
        public string? Message { get; set; }
        public DateOnly? NotifDate { get; set; }
        public int? EventId { get; set; }
        public int? UserId { get; set; }
    }

}
