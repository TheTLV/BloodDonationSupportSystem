namespace BloodDonationSupportSystem.DTOs
{
    public class BloodDonationDTO
    {
        public int UserId { get; set; }
        public string BloodType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateOnly DonationDate { get; set; }
        public TimeOnly DonationTime { get; set; }
        public string Status { get; set; } = "Pending"; 
    }
}
