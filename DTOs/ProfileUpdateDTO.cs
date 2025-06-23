namespace BloodDonationSupportSystem.DTOs
{
    public class ProfileUpdateDTO // dành cho user xem thông tin cá nhân và chỉnh sửa thông tin cá nhân của mình
    {

        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? BloodGroup { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Address { get; set; }
    }
}
