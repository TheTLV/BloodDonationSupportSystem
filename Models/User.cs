using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection.Metadata;

namespace BloodDonationSupportSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Profile Profile { get; set; }
        public ICollection<BloodRequest> BloodRequests { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}