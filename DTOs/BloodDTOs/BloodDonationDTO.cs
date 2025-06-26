namespace BloodDonationSupportSystem.DTOs.BloodDTO
{
    public class BloodDonationDTO
    {
        public string BloodType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateOnly DonationDate { get; set; }
        public TimeOnly DonationTime { get; set; }
        public string Status { get; set; } = "Pending"; 
    }
}
