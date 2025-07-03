using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IBlogRepository
    {
        Task<int> CreateAsync(Blog blog);
        Task<Blog?> GetByIdAsync(int id);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<IEnumerable<Blog>> SearchAsync(string keyword);
        Task<bool> UpdateAsync(Blog blog);
        Task<bool> DeleteAsync(int id);
    }

}
