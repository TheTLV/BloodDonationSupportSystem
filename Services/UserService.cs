using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<List<UserViewDTO>> GetAllUsersAsync()
        //{
        //    var users = await _context.Users
        //        .Include(u => u.Role)
        //        .Select(u => new UserViewDTO
        //        {
        //            UserId = u.UserId,
        //            Fullname = u.Fullname,
        //            Email = u.Email,
        //            PhoneNumber = u.PhoneNumber,
        //            RoleName = u.Role!.RoleName!
        //        }).ToListAsync();

        //    return users;
        //}

        //public async Task<UserDetailDTO?> GetUserDetailAsync(int id)
        //{
        //    var user = await _context.Users
        //        .Include(u => u.Role)
        //        .Include(u => u.Profile)
        //        .FirstOrDefaultAsync(u => u.UserId == id);

        //    if (user == null) return null;

        //    return new UserDetailDTO
        //    {
        //        UserId = user.UserId,
        //        Fullname = user.Fullname,
        //        Email = user.Email,
        //        PhoneNumber = user.PhoneNumber,
        //        RoleName = user.Role?.RoleName ?? "Unknown",

        //        // Profile data
        //        BloodGroup = user.Profile?.BloodGroup,
        //        Address = user.Profile?.Address,
        //        Gender = user.Profile?.Gender,
        //        DateOfBirth = user.Profile?.DateOfBirth,
        //        LastDonationDate = user.Profile?.LastDonationDate,
        //        LastReceivedDate = user.Profile?.LastReceivedDate
        //    };
        //}

        //public async Task<bool> DeleteUserAsync(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null) return false;

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<UserViewDTO> UpdateUserAsync(int id, UserUpdateDTO dto)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null) throw new Exception("User not found");

        //    user.Fullname = dto.Fullname;
        //    user.Email = dto.Email;
        //    user.PhoneNumber = dto.PhoneNumber;
        //    user.RId = dto.RoleId;

        //    await _context.SaveChangesAsync();

        //    return new UserViewDTO
        //    {
        //        UserId = user.UserId,
        //        Fullname = user.Fullname,
        //        Email = user.Email,
        //        PhoneNumber = user.PhoneNumber,
        //        RoleName = (await _context.Roles.FindAsync(dto.RoleId))?.RoleName ?? "Unknown"
        //    };
        //}

        public async Task<UserDetailDTO?> GetOwnProfileAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return null;

            return new UserDetailDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleName = user.Role?.RoleName ?? "Unknown",
                BloodGroup = user.Profile?.BloodGroup,
                Address = user.Profile?.Address,
                Gender = user.Profile?.Gender,
                DateOfBirth = user.Profile?.DateOfBirth,
                LastDonationDate = user.Profile?.LastDonationDate,
                LastReceivedDate = user.Profile?.LastReceivedDate
            };
        }


        public async Task<bool> UpdateOwnProfileAsync(int userId, ProfileUpdateDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return false;

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
            return true;
        }
    }
}
