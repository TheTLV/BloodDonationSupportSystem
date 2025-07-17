namespace BloodDonationSupportSystem.DTOs.NotificationDTOs
{
    public class NotificationUnreadCountDTO
    {
        public int RealCount { get; set; }
        public string DisplayCount => RealCount > 9 ? "9+" : RealCount.ToString();
    }
}
