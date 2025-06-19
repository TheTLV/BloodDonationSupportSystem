namespace BloodDonationSupportSystem.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; } // "Admin", "Staff", "Member", "Guest"
        public ICollection<User> Users { get; set; }
    }
}