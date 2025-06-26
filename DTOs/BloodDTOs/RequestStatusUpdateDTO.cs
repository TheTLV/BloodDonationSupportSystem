namespace BloodDonationSupportSystem.DTOs.BloodDTO
{
    public class RequestStatusUpdateDTO
    {
        public int RequestId { get; set; }
        public string Status { get; set; } = null!;
    }
}
