using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.UsersDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }


        public void UpdateMyBloodRequest(RequestUpdateDTO dto, int userId)
        {
            var request = _context.Bloodrequests.FirstOrDefault(r => r.RequestId == dto.RequestId && r.UserId == userId);
            if (request == null)
                throw new Exception("Request not found or access denied");
            request.Quantity = dto.Quantity ?? request.Quantity;
            request.RequestDate = dto.RequestDate ?? request.RequestDate;
            request.RequestTime = dto.RequestTime ?? request.RequestTime;
            _context.SaveChanges();
        }

        public async Task<bool> DeleteMyBloodRequestAsync(int requestId, int userId)
        {
            var request = await _context.Bloodrequests
                .FirstOrDefaultAsync(r => r.RequestId == requestId && r.UserId == userId);

            if (request == null)
                return false;

            _context.Bloodrequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMyDonationAsync(int donationId, int userId)
        {
            var donation = await _context.Donations
                .FirstOrDefaultAsync(d => d.DonationId == donationId && d.UserId == userId);

            if (donation == null)
                return false;

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            return true;
        }

        public void UpdateMyDonation(int donationId, DonationUpdateDTO dto, int userId)
        {
            var donation = _context.Donations.FirstOrDefault(d => d.DonationId == donationId && d.UserId == userId);
            if (donation == null)
                throw new Exception("Donation not found or access denied");

            donation.Quantity = dto.Quantity ?? donation.Quantity;
            donation.DonationDate = dto.DonationDate ?? donation.DonationDate;
            donation.DonationTime = dto.DonationTime ?? donation.DonationTime;
            _context.SaveChanges();
        }


        public async Task<UserDetailDTO> GetOwnProfileAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.UserId == userId);

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
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                throw new Exception("User not found");

            user.Fullname = dto.Fullname ?? user.Fullname;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

            if (user.Profile == null)
            {
                user.Profile = new Profile { UserId = userId };
                _context.Profiles.Add(user.Profile);
            }

            user.Profile.Address = dto.Address ?? user.Profile.Address;
            user.Profile.Gender = dto.Gender ?? user.Profile.Gender;
            user.Profile.BloodGroup = dto.BloodGroup ?? user.Profile.BloodGroup;
            user.Profile.DateOfBirth = dto.DateOfBirth ?? user.Profile.DateOfBirth;

            await _context.SaveChangesAsync();
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
            return await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserViewDTO
                {
                    UserId = u.UserId,
                    Fullname = u.Fullname,
                    Email = u.Email,
                    RoleName = u.Role.RoleName
                })
                .ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserRoleAsync(int userId, int newRoleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.RoleId = newRoleId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDetailDTO> GetUserDetailByIdAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.UserId == userId);

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
