namespace BloodDonationSupportSystem.DTOs
{
    public class DonationUpdateDTO
    {
        public int? DonationId { get; set; }
        public int? Quantity { get; set; }
        public DateOnly? DonationDate { get; set; }
        public TimeOnly? DonationTime { get; set; }
    }
}
