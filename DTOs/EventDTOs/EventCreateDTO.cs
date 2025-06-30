namespace BloodDonationSupportSystem.DTOs.EventDTOs
{
    public class EventCreateDTO
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly EventDate { get; set; }
    }
}
