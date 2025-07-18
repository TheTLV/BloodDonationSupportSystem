﻿namespace BloodDonationSupportSystem.DTOs.EventDTOs
{
    public class EventDetailDTO
    {
        public int EventId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateOnly EventDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
