using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IDonationEligibilityService
    {
        Task<DonationEligibility> CreateEligibilityAsync(int userId, int donationId);
        Task<bool> UpdateBasicScreeningAsync(int eligibilityId, bool passed, string notes);
        Task<bool> UpdateMedicalScreeningAsync(int eligibilityId, DonationEligibilityDTO dto);
    }
}
