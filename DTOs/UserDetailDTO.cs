namespace BloodDonationSupportSystem.DTOs
{
    public class UserDetailDTO
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public string RoleName { get; set; } = null!;

        // Profile info
        public string? BloodGroup { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? LastDonationDate { get; set; }
        public DateOnly? LastReceivedDate { get; set; }
    }
}
