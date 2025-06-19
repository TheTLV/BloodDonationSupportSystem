namespace BloodDonationSupportSystem.Models
{
    public class BloodRequest
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string BloodType { get; set; }
        public int Quantity { get; set; }
        public string Urgency { get; set; } // "Normal", "Emergency"
        public string Location { get; set; }
        public string Status { get; set; } // "Pending", "Matched", "Completed"
        public DateTime RequestDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}