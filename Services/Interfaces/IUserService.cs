using BloodDonationSupportSystem.DTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailDTO?> GetOwnProfileAsync(int userId);
        Task<bool> UpdateOwnProfileAsync(int userId, ProfileUpdateDTO dto);

    }
}
