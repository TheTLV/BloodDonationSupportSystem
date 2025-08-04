namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class DoctorDonationViewDTO
    {
        public int donationId { get; set; }
        public int UserId { get; set; }
        public string? BloodGroup { get; set; }
        public string? Status { get; set; }
        public int? Quantity { get; set; }
        public DateOnly DonationDate { get; set; }
        public TimeOnly DonationTime { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public bool? IsBloodGroupVerified { get; set; }
    }
}
