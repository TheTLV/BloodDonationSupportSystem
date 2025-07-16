namespace BloodDonationSupportSystem.DTOs.NotificationDTOs
{
    public class AdminNotificationCreateDTO
    {
        public string Message { get; set; } = string.Empty;
        public int? UserId { get; set; } = null;
    }
}
