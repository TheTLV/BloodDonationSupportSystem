namespace BloodDonationSupportSystem.DTOs.BloodDTO
{
    public class BloodRequestDTO
    {
        public string BloodType { get; set; } = null!;
        public int Quantity { get; set; }
        public DateOnly RequestDate { get; set; }
        public TimeOnly RequestTime { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
