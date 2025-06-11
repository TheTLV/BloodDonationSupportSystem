namespace BloodDonationSupportSystem.Models
{
    public class User
    {
        
        public int UID { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int? PhoneNumber { get; set; } 

        public int Role { get; set; } = 1;// e.g., "User", "Staff", "Admin"


    }
}
