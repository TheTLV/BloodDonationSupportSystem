namespace BloodDonationSupportSystem.DTOs
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public int? EventId { get; set; }
        public string Message { get; set; } = null!;
        public DateOnly? NotifDate { get; set; }
        public bool IsActionRequired { get; set; }

        // Optional, sẽ chỉ có giá trị khi IsActionRequired = true
        public string? ResponseStatus { get; set; }
    }

}
