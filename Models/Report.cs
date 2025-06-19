namespace BloodDonationSupportSystem.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime GeneratedDate { get; set; }
    }
}