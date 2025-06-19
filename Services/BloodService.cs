using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;

namespace BloodDonationSupportSystem.Services
{
    public class BloodService : IBloodService
    {
        private readonly AppDbContext _context;

        public BloodService(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateRequest(BloodRequestDTO dto)
        {
            var request = new BloodRequest
            {
                UserId = dto.UserId,
                BloodGroup = dto.BloodType,
                Quantity = dto.Quantity,
                RequestDate = DateTime.Now
            };

            _context.Bloodrequests.Add(request);
            return _context.SaveChanges() > 0;
        }

        public bool CreateDonation(BloodDonationDTO dto)
        {
            var donation = new Donation
            {
                UserId = dto.UserId,
                BloodGroup = dto.BloodType,
                Quantity = dto.Quantity,
                DonationDate = DateTime.Now
            };

            _context.Donations.Add(donation);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Donation> GetDonationsByUserId(int userId)
        {
            return _context.Donations.Where(d => d.UserId == userId).ToList();
        }

        public IEnumerable<BloodRequest> GetRequestsByUserId(int userId)
        {
            return _context.Bloodrequests.Where(r => r.UserId == userId).ToList();
        }
    }
}
