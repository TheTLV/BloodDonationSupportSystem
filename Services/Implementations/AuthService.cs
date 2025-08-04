using System.Net;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml.Linq;
using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.DTOs.AuthDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IProfileRepository _profileRepo;

        public AuthService(IUserRepository userRepo, IProfileRepository profileRepo)
        {
            _userRepo = userRepo;
            _profileRepo = profileRepo;
        }

        public async Task<User> Login(UserLoginDTO dto)
        {
            var user = await _userRepo.GetByEmailAndPassword(dto.Email, dto.Password);

            if (user == null)
                throw new Exception("Sai tài khoản hoặc mật khẩu");
            
            if (user.StatusId != 1) 
                throw new Exception($"Tài khoản không được phép đăng nhập: {user.Status?.StatusName ?? "Không rõ lý do"}");

            if (user.Role == null)
                throw new Exception("User không có Role, dữ liệu sai");

            return user;
        }

        public async Task<User> Register(UserRegisterDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email không được để trống");

            var existingEmail = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingEmail != null)
                throw new Exception("Email đã được sử dụng");

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                var existingPhone = await _userRepo.GetByPhoneAsync(dto.PhoneNumber!);
                if (existingPhone != null)
                    throw new Exception("Số điện thoại đã được sử dụng");
            }
            var user = new User
            {
                Fullname = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                RoleId = 1
            };

            var profile = new Profile
            {
                User = user,
                Gender = dto.Gender,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth
            };

            await _userRepo.AddUserAsync(user);         
            _profileRepo.AddProfile(profile); 

            return user;
        }


    }
}
