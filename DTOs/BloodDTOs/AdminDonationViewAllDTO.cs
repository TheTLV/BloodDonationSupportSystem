namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class AdminDonationViewAllDTO
    {
        public int DonationId { get; set; }
        public int? UserId { get; set; }
        public string? Fullname { get; set; }
        public string? Status { get; set; }
        public string? BloodGroup { get; set; }
        public int? Quantity { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? DonationDate { get; set; }
        public TimeOnly? DonationTime { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? ChronicDisease { get; set; }
        public string? Medication { get; set; }
    }
}
