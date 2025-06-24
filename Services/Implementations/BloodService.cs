using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class BloodService : IBloodService
    {
        private readonly AppDbContext _context;

        public BloodService(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateRequest(int userId, BloodRequestDTO dto)
        {
            var request = new Bloodrequest
            {
                UserId = userId,
                BloodGroup = dto.BloodType,
                Quantity = dto.Quantity,
                RequestDate = dto.RequestDate,
                RequestTime = dto.RequestTime
            };

            _context.Bloodrequests.Add(request);
            return _context.SaveChanges() > 0;
        }

        public bool CreateDonation(int userId ,BloodDonationDTO dto)
        {
            var donation = new Donation
            {
                UserId = userId,
                BloodGroup = dto.BloodType,
                Quantity = dto.Quantity,
                DonationDate = dto.DonationDate,
                DonationTime = dto.DonationTime
            };

            _context.Donations.Add(donation);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Donation> GetDonationsByUserId(int userId)
        {
            return _context.Donations.Where(d => d.UserId == userId).ToList();
        }

        public IEnumerable<Bloodrequest> GetRequestsByUserId(int userId)
        {
            return _context.Bloodrequests.Where(r => r.UserId == userId).ToList();
        }
    }
}
