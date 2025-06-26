namespace BloodDonationSupportSystem.DTOs.UsersDTOs
{
    public class ProfileUpdateDTO
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BloodGroup { get; set; }
        public DateOnly? DateOfBirth { get; set; } 
        public string? Address { get; set; }
    }
}
