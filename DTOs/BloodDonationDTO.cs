namespace BloodDonationSupportSystem.DTOs
{
    public class BloodDonationDTO
    {
        public int UserId { get; set; }
        public string BloodType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateOnly RequestDate { get; set; }
    }
}
