namespace BloodDonationSupportSystem.DTOs
{
    public class UserViewDTO
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
