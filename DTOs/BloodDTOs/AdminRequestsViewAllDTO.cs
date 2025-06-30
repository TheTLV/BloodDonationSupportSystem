namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class AdminRequestsViewAllDTO
    {
        public int RequestId { get; set; }
        public int? UserId { get; set; }
        public string? Fullname { get; set; }
        public string? Status { get; set; }
        public string? BloodGroup { get; set; }
        public int? Quantity { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? RequestDate { get; set; }
        public TimeOnly? RequestTime { get; set; }
    }
}
