namespace BloodDonationSupportSystem.DTOs
{
    public class RequestStatusUpdateDTO
    {
        public int RequestId { get; set; }
        public string Status { get; set; } = null!;
    }
}
