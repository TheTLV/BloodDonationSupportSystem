namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class DonationViewDTO
    {

        public string? BloodGroup { get; set; }

        public string? Status { get; set; }

        public int? Quantity { get; set; }

        public DateOnly DonationDate { get; set; }

        public TimeOnly DonationTime { get; set; }
    }
}
