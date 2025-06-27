using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.UsersDTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailDTO> GetOwnProfileAsync(int userId);
        Task<ProfileUpdateDTO> UpdateMyProfileAsync(int userId, ProfileUpdateDTO dto);
        void UpdateMyDonation(int id, DonationUpdateDTO dto, int userId);
        void CancelMyDonation(int donationId, int userId);

        void UpdateMyBloodRequest(RequestUpdateDTO dto, int userId);
        void CancelMyBloodRequest(int requestId, int userId);


    }
}
