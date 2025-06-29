namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class RequestsViewDTO
    {

        public string? BloodGroup { get; set; }

        public string? Status { get; set; }

        public int? Quantity { get; set; }

        public DateOnly? RequestDate { get; set; }

        public TimeOnly? RequestTime { get; set; }
    }
}
