namespace BloodDonationSupportSystem.DTOs
{
    public class DonationStatusUpdateDTO
    {
        public int DonationId { get; set; }
        public string Status { get; set; } = null!; 
    }
}
