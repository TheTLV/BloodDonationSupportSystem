using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.UsersDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UpdateMyBloodRequestAsync(RequestUpdateDTO dto, int userId)
        {
            var request = await _userRepository.GetRequestByIdAndUserAsync(dto.RequestId, userId);
            if (request == null)
                throw new Exception("Request not found or access denied");

            request.Quantity = dto.Quantity ?? request.Quantity;
            request.RequestDate = dto.RequestDate ?? request.RequestDate;
            request.RequestTime = dto.RequestTime ?? request.RequestTime;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteMyBloodRequestAsync(int requestId, int userId)
        {
            var request = await _userRepository.GetRequestByIdAndUserAsync(requestId, userId);
            if (request == null) return false;

            _userRepository.RemoveBloodRequest(request);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMyDonationAsync(int donationId, int userId)
        {
            var donation = await _userRepository.GetDonationByIdAndUserAsync(donationId, userId);
            if (donation == null) return false;

            _userRepository.RemoveDonation(donation);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task UpdateMyDonationAsync(int donationId, DonationUpdateDTO dto, int userId)
        {
            var donation = await _userRepository.GetDonationByIdAndUserAsync(donationId, userId);
            if (donation == null)
                throw new Exception("Donation not found or access denied");

            donation.Quantity = dto.Quantity ?? donation.Quantity;
            donation.DonationDate = dto.DonationDate ?? donation.DonationDate;
            donation.DonationTime = dto.DonationTime ?? donation.DonationTime;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserDetailDTO> GetOwnProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserWithRoleAndProfileAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            return new UserDetailDTO
            {
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleName = user.Role?.RoleName ?? "Unknown",
                BloodGroup = user.Profile?.BloodGroup,
                Address = user.Profile?.Address,
                Gender = user.Profile?.Gender,
                DateOfBirth = user.Profile?.DateOfBirth
            };
        }

        public async Task<ProfileUpdateDTO> UpdateMyProfileAsync(int userId, ProfileUpdateDTO dto)
        {
            var user = await _userRepository.GetUserWithProfileAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.Fullname = dto.Fullname ?? user.Fullname;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

            if (user.Profile == null)
            {
                user.Profile = new Profile { UserId = userId };
                _userRepository.AddProfile(user.Profile);
            }

            user.Profile.Address = dto.Address ?? user.Profile.Address;
            user.Profile.Gender = dto.Gender ?? user.Profile.Gender;
            user.Profile.BloodGroup = dto.BloodGroup ?? user.Profile.BloodGroup;
            user.Profile.DateOfBirth = dto.DateOfBirth ?? user.Profile.DateOfBirth;

            await _userRepository.SaveChangesAsync();
            return new ProfileUpdateDTO
            {
                Fullname = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Gender = user.Profile.Gender,
                BloodGroup = user.Profile.BloodGroup,
                DateOfBirth = user.Profile.DateOfBirth,
                Address = user.Profile.Address
            };
        }

        public async Task<IEnumerable<UserViewDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserViewDTO
            {
                UserId = u.UserId,
                Fullname = u.Fullname,
                Email = u.Email,
                BloodGroup = u.Profile?.BloodGroup,
                PhoneNumber = u.PhoneNumber,
                RoleName = u.Role?.RoleName ?? "Unknown",
                Status = u.Status?.StatusName ?? "Unknown"
            });
        }

        public async Task<bool> UpdateUserStatusAsync(int userId, int newStatusId)
        {
            return await _userRepository.UpdateUserStatusAsync(userId, newStatusId);
        }


        //public async Task<bool> UpdateUserRoleAsync(int userId, int newRoleId)
        //{
        //    return await _userRepository.UpdateUserRoleAsync(userId, newRoleId);
        //}
        public async Task<bool> UpdateUserRoleAsync(int userId, int newRoleId)
        {
            if (newRoleId < 1 || newRoleId > 4)
                throw new ArgumentOutOfRangeException(nameof(newRoleId), "RoleId phải nằm giữa 1 và 4.");

            return await _userRepository.UpdateUserRoleAsync(userId, newRoleId);
        }


        public async Task<UserDetailDTO> GetUserDetailByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserWithRoleAndProfileAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            return new UserDetailDTO
            {
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleName = user.Role?.RoleName ?? "Unknown",
                BloodGroup = user.Profile?.BloodGroup,
                Address = user.Profile?.Address,
                Gender = user.Profile?.Gender,
                DateOfBirth = user.Profile?.DateOfBirth
            };
        }
    }
}
