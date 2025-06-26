namespace BloodDonationSupportSystem.DTOs.UsersDTOs
{
    public class UserUpdateDTO
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? BloodGroup { get; set; }
        public int RoleId { get; set; }
    }

}
