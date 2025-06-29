using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IBloodService
    {
        bool CreateDonation(int userId, BloodDonationDTO dto);
        IEnumerable<DonationViewDTO> GetDonationsByUserId(int userId);
        Task<DonateDetailDTO> GetDonate(int id);
        Task<RequestDetailDTO> GetRequest(int id);

        bool CreateRequest(int userId , BloodRequestDTO dto);
        IEnumerable<RequestsViewDTO> GetRequestsByUserId(int userId);

        // --- Admin Functions ---

        // Donation
        Task<IEnumerable<DonationViewDTO>> GetAllDonationsForAdmin();
        Task<IEnumerable<DonationViewDTO>> SearchDonations(string? bloodGroup, string? status);
        Task<bool> UpdateDonationStatusAsync(int donationId, string newStatus);

        // Request
        Task<IEnumerable<RequestsViewDTO>> GetAllRequestsForAdmin();
        Task<IEnumerable<RequestsViewDTO>> SearchRequests(string? bloodGroup, string? status);
        Task<bool> UpdateRequestStatusAsync(int requestId, string newStatus);
    }
}
