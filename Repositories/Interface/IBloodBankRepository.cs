using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IBloodBankRepository
    {
        Task<BloodBank?> GetByIdAsync(int bankId);
        Task<IEnumerable<BloodBank>> GetAllAsync();
        Task<BloodBank?> GetByBloodGroupAsync(string bloodGroup);
        Task<bool> HasMinimumQuantityAsync(string bloodGroup, int minQuantityMl);
        Task AddAsync(BloodBank bloodBank);
        Task UpdateAsync(BloodBank bloodBank);
        Task DeleteAsync(int bankId);
        Task<bool> TryAdjustQuantityAsync(string bloodGroup, int deltaMl);
        Task<bool> SaveChangesAsync();
    }
}
