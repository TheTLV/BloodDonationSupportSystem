namespace BloodDonationSupportSystem.Models
{
    public class Donation
    {
        public int DonationId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime DonationDate { get; set; }
        public string Location { get; set; }
        public int Volume { get; set; }
        public string Notes { get; set; }
    }
}