namespace BloodDonationSupportSystem.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FullName { get; set; }
        public string BloodType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? LastDonationDate { get; set; }
    }
}