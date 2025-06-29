using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.UsersDTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailDTO> GetOwnProfileAsync(int userId);
        Task<ProfileUpdateDTO> UpdateMyProfileAsync(int userId, ProfileUpdateDTO dto);
        void UpdateMyDonation(int id, DonationUpdateDTO dto, int userId);

        void UpdateMyBloodRequest(RequestUpdateDTO dto, int userId);
        Task<bool> DeleteMyBloodRequestAsync(int requestId, int userId);
        Task<bool> DeleteMyDonationAsync(int donationId, int userId);

        Task<IEnumerable<UserViewDTO>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UpdateUserRoleAsync(int userId, int newRoleId);
        Task<UserDetailDTO> GetUserDetailByIdAsync(int userId);

    }
}
