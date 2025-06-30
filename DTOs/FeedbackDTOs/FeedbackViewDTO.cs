namespace BloodDonationSupportSystem.DTOs.FeedbackDTOs
{
    public class FeedbackViewDTO
    {
        public int FeedbackId { get; set; }

        public int CreatedBy { get; set; }

        public required string CreatedByName { get; set; }
        public string Email { get; set; } = "";

        public string? Content { get; set; }

        public DateOnly? FeedbackDate { get; set; }
    }
}
