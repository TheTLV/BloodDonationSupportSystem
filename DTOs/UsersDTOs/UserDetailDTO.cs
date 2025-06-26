namespace BloodDonationSupportSystem.DTOs.UsersDTOs
{
    public class UserDetailDTO // dành cho admin , staff và user xem thông tin cá nhân chi tiết của user 
    {
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public string RoleName { get; set; } = null!;

        // Profile info
        public string? BloodGroup { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}
