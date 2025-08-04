using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IDonationEligibilityRepository
    {
        Task<DonationEligibility?> GetByIdAsync(int eligibilityId);
        Task<IEnumerable<DonationEligibility>> GetByUserIdAsync(int userId);
        Task<IEnumerable<DonationEligibility>> GetByDonationIdAsync(int donationId);
        Task<DonationEligibility?> GetLatestByUserIdAsync(int userId);
        Task AddAsync(DonationEligibility eligibility);
        Task UpdateAsync(DonationEligibility eligibility);
        Task DeleteAsync(int eligibilityId);
        Task<bool> SaveChangesAsync();
    }
}
