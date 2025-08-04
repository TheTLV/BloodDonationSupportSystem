using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Repositories.Interface
{
    public interface IProfileRepository
    {
        void AddProfile(Profile profile);
        Task UpdateBloodGroupAsync(int userId, string bloodGroup);
    }
}
