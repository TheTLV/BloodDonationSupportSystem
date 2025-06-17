using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IBloodRequestService
    {
        bool CreateRequest(BloodRequestDTO dto);
        IEnumerable<BloodRequest> GetByUserId(int userId);
    }
}
