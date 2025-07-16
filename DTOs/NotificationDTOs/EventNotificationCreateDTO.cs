namespace BloodDonationSupportSystem.DTOs.NotificationDTOs
{
    public class EventNotificationCreateDTO
    {
        public string Message { get; set; } = string.Empty;
        public int? EventId { get; set; } = null;
    }
}
