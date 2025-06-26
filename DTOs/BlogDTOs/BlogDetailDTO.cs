namespace BloodDonationSupportSystem.DTOs.BlogDTOs
{
    public class BlogDetailDTO
    {
        public int BlogId { get; set; }
        public required string Title { get; set; }

        public required string Image { get; set; }

        public required string Link { get; set; }

        public required string Description { get; set; }
    }
}
