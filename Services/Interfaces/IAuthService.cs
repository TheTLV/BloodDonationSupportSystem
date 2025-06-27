using BloodDonationSupportSystem.DTOs.AuthDTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IAuthService
    {
        User Login(UserLoginDTO dto);
        User Register(UserRegisterDTO dto);
    }
}
