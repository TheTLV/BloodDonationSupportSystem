using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IBloodService
    {
        bool CreateDonation(int userId, BloodDonationDTO dto);
        IEnumerable<Donation> GetDonationsByUserId(int userId);

        bool CreateRequest(int userId , BloodRequestDTO dto);
        IEnumerable<Bloodrequest> GetRequestsByUserId(int userId);
    }
}
