namespace BloodDonationSupportSystem.DTOs
{
    public class BlogDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}