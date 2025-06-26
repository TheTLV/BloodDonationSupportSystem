namespace BloodDonationSupportSystem.DTOs.BlogDTOs
{
    public class BlogUpdateDTO
    {
        public int BlogId { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }

    }
}
