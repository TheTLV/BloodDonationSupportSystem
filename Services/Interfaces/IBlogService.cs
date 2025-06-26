using BloodDonationSupportSystem.DTOs.BlogDTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IBlogService
    {
        Task<int> CreateBlogAsync(BlogCreateDTO dto);
        Task<bool> UpdateBlogAsync(int blogId, BlogUpdateDTO dto);
        Task<bool> DeleteBlogAsync(int blogId);
        Task<IEnumerable<BlogDetailDTO>> GetAllBlogsAsync();
        Task<BlogDetailDTO?> GetBlogByIdAsync(int blogId);
        Task<IEnumerable<BlogDetailDTO>> SearchBlogsAsync(string keyword);
    }
}
