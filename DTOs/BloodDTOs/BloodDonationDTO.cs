namespace BloodDonationSupportSystem.DTOs.BloodDTO
{
    public class BloodDonationDTO
    {
        public string BloodType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateOnly DonationDate { get; set; }
        public TimeOnly DonationTime { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string? ChronicDisease { get; set; }
        public string? Medication { get; set; }
        public DateOnly? LastDonationDate { get; set; } 
    }
}
