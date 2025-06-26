namespace BloodDonationSupportSystem.DTOs.UsersDTOs
{
    public class UserViewDTO // dành cho admin và staff xem list user
    {
        public int UserId { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? BloodGroup { get; set; }
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
