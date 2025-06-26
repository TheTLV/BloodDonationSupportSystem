namespace BloodDonationSupportSystem.DTOs.BloodDTO
{
    public class RequestUpdateDTO
    {
        public int? RequestId { get; set; }
        public int? Quantity { get; set; }
        public DateOnly? RequestDate { get; set; }
        public TimeOnly? RequestTime { get; set; }
    }
}
