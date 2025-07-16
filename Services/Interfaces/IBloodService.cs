using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IBloodService
    {
        Task<bool> CreateDonationAsync(int userId, BloodDonationDTO dto);
        IEnumerable<DonationViewDTO> GetDonationsByUserId(int userId);
        Task<DonateDetailDTO> GetDonate(int id);
        Task<RequestDetailDTO> GetRequest(int id);

        Task<bool> CreateRequestAsync(int userId, BloodRequestDTO dto);
        IEnumerable<RequestsViewDTO> GetRequestsByUserId(int userId);

        // --- Admin Functions ---

        // Donation
        Task<IEnumerable<AdminDonationViewAllDTO>> GetAllDonationsForAdmin();
        Task<IEnumerable<DonationViewDTO>> SearchDonations(string? bloodGroup, string? status);
        Task<bool> UpdateDonationStatusAsync(int donationId, string newStatus);

        // Request
        Task<IEnumerable<AdminRequestsViewAllDTO>> GetAllRequestsForAdmin();
        Task<IEnumerable<RequestsViewDTO>> SearchRequests(string? bloodGroup, string? status);
        Task<bool> UpdateRequestStatusAsync(int requestId, string newStatus);
    }
}
