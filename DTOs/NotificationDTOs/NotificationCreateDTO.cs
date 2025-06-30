namespace BloodDonationSupportSystem.DTOs.NotificationDTOs
{
    public class NotificationCreateDTO
    {
        public string Message { get; set; } = string.Empty;
        public int? EventId { get; set; } 
    }
}
