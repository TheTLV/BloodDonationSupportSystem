namespace BloodDonationSupportSystem.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // "Upcoming", "Ongoing", "Completed"
    }
}