using BloodDonationSupportSystem.DTOs.AuthDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Login(UserLoginDTO dto);
        Task<User> Register(UserRegisterDTO dto);
    }
}
