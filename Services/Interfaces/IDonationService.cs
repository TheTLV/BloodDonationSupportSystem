using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IDonationService
    {
        bool CreateDonation(BloodDonationDTO dto);
        IEnumerable<Donation> GetByUserId(int userId);
    }
}
