namespace BloodDonationSupportSystem.DTOs
{
    public class BloodRequestDto
    {
        public int RequestId { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
        public string Urgency { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequesterName { get; set; }
    }
}