namespace BloodDonationSupportSystem.DTOs.EventDTOs
{
    public class EventUpdateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly EventDate { get; set; }
    }
}
