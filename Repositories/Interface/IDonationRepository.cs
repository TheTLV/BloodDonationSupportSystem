using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IDonationRepository
    {
        void Add(Donation donation);
        Task<Donation?> GetByIdAsync(int id);
        IQueryable<Donation> GetAll();
        Task<bool> SaveChangesAsync();
        Task UpdateAsync(Donation donation);
        Task<IEnumerable<Donation>> GetPendingDonationsAsync();
    }
}
