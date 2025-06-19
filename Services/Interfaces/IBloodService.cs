using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IBloodService
    {
        bool CreateDonation(BloodDonationDTO dto);
        IEnumerable<Donation> GetDonationsByUserId(int userId);

        bool CreateRequest(BloodRequestDTO dto);
        IEnumerable<BloodRequest> GetRequestsByUserId(int userId);
    }
}
