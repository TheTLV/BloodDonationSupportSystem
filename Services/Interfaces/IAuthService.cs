using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IAuthService
    {
        User Login(string email, string password);
        User Register(string name ,string email , string password , string phone  );
    }
}
