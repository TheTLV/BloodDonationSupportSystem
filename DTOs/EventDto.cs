namespace BloodDonationSupportSystem.DTOs
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string OrganizerName { get; set; }
    }
}