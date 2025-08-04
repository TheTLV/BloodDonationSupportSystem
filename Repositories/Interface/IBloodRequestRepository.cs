using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IBloodRequestRepository
    {
        void Add(Bloodrequest request);
        Task<Bloodrequest?> GetByIdAsync(int id);
        IQueryable<Bloodrequest> GetAll();
        Task<bool> SaveChangesAsync();
        Task UpdateAsync(Bloodrequest request);
    }
}
