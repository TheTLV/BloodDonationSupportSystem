using BloodDonationSupportSystem.DTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserViewDTO>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
        Task<UserViewDTO> UpdateUserAsync(int id, UserUpdateDTO dto);

    }
}
