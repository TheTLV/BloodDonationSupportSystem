using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IBloodService
    {
        Task<bool> CreateDonationAsync(int userId, BloodDonationDTO dto);
        Task<bool> CreateRequestAsync(int userId, BloodRequestDTO dto);
        IEnumerable<DonationViewDTO> GetDonationsByUserId(int userId);
        IEnumerable<RequestsViewDTO> GetRequestsByUserId(int userId);
        Task<DonateDetailDTO> GetDonate(int id);
        Task<RequestDetailDTO> GetRequest(int id);


        // --- Admin Functions ---

        // Donation
        Task<IEnumerable<AdminDonationViewAllDTO>> GetAllDonationsForAdmin();
        Task<IEnumerable<DonationViewDTO>> SearchDonations(string? bloodGroup, string? status);
        //Task<bool> UpdateDonationStatusAsync(int donationId, string newStatus);
        Task<(bool Success, string? Error)> UpdateDonationStatusAsync(int donationId, string newStatus);
        // Request
        Task<IEnumerable<AdminRequestsViewAllDTO>> GetAllRequestsForAdmin();
        Task<IEnumerable<RequestsViewDTO>> SearchRequests(string? bloodGroup, string? status);
        Task<bool> UpdateRequestStatusAsync(int requestId, string newStatus);

        // --- Doctor Functions ---
        Task<bool> ProcessBloodQualityCheckAsync(int donationId, bool qualityPassed);
        Task<IEnumerable<DoctorDonationViewDTO>> GetPendingDonationsForDoctor();
        Task<bool> UpdateUserBloodGroupAsync(int userId, UpdateBloodTypeDTO dto);

        Task AddOrUpdateStockAsync(string bloodGroup, int quantityMl);
        Task<BloodBank?> GetStockByGroupAsync(string bloodGroup);
        Task<IEnumerable<BloodBank>> GetAllStocksAsync();
        Task<DoctorNoteDTO> DoctorNoteAsync(int EId, DoctorNoteDTO note);
        Task<IEnumerable<DoctorNoteDTO>> GetDoctorNotesAsync(int EId);



        //Task<bool> ProcessMedicalScreeningAsync(int donationId, DonationEligibilityDTO dto);
        Task<(bool Success, string? Error)> ProcessBloodAnalysisAsync(int donationId, PostDonationAnalysisDTO dto);
        Task<(bool Success, string? Error)> ProcessMedicalScreeningAsync(int donationId, PreDonationScreeningDTO dto);
    }
}
