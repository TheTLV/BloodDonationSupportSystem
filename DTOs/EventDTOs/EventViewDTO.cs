namespace BloodDonationSupportSystem.DTOs.EventDTOs
{
    public class EventViewDTO
    {
        public int EventId { get; set; }
        public required string Title { get; set; }
        public DateOnly EventDate { get; set; }
    }
}
