using BloodDonationSupportSystem.DTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailDTO?> GetOwnProfileAsync(int userId);
        Task<bool> UpdateMyProfileAsync(int userId, ProfileUpdateDTO dto);
        void UpdateMyDonation(DonationUpdateDTO dto, int userId);
        void CancelMyDonation(int donationId, int userId);

        void UpdateMyBloodRequest(RequestUpdateDTO dto, int userId);
        void CancelMyBloodRequest(int requestId, int userId);


    }
}
