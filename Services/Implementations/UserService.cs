using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
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

        public void CancelMyBloodRequest(int requestsId, int userId)
        {
            var request = _context.Bloodrequests.FirstOrDefault(d => d.RequestId == requestsId && d.UserId == userId);
            if (request == null)
                throw new Exception("Request not found or access denied");

            request.Status = "cancelled";
            _context.SaveChanges();
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


        public void CancelMyDonation(int donationId, int userId)
        {
            var donation = _context.Donations.FirstOrDefault(d => d.DonationId == donationId && d.UserId == userId);
            if (donation == null)
                throw new Exception("Donation not found or access denied");

            donation.Status = "cancelled";
            _context.SaveChanges();
        }

        public void UpdateMyDonation(DonationUpdateDTO dto, int userId)
        {
            var donation = _context.Donations.FirstOrDefault(d => d.DonationId == dto.DonationId && d.UserId == userId);
            if (donation == null)
                throw new Exception("Donation not found or access denied");
            donation.Quantity = dto.Quantity ?? donation.Quantity;
            donation.DonationDate = dto.DonationDate ?? donation.DonationDate;
            donation.DonationTime = dto.DonationTime ?? donation.DonationTime;
            _context.SaveChanges();
        }


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


        public async Task<bool> UpdateMyProfileAsync(int userId, ProfileUpdateDTO dto)
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
