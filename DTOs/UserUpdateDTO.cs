namespace BloodDonationSupportSystem.DTOs
{
    public class UserUpdateDTO
    {
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }

}
