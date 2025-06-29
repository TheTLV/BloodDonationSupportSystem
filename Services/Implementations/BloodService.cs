using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                RequestTime = dto.RequestTime,
                Status = "pending" 
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
                DonationTime = dto.DonationTime,
                Status = "pending"
            };

            _context.Donations.Add(donation);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<DonationViewDTO> GetDonationsByUserId(int userId)
        {
            var donations = _context.Donations
                .Where(d => d.UserId == userId)
                .Select(d => new DonationViewDTO
                {
                    BloodGroup = d.BloodGroup,
                    Status = d.Status,
                    Quantity = d.Quantity,
                    DonationDate = d.DonationDate,
                    DonationTime = d.DonationTime
                })
                .ToList();

            return donations;
        }

        public IEnumerable<RequestsViewDTO> GetRequestsByUserId(int userId)
        {
            return _context.Bloodrequests
                .Where(r => r.UserId == userId)
                .Select(r => new RequestsViewDTO
                {
                    BloodGroup = r.BloodGroup,
                    Status = r.Status,
                    Quantity = r.Quantity,
                    RequestDate = r.RequestDate,
                    RequestTime = r.RequestTime
                })
                .ToList();
        }

        public async Task<DonateDetailDTO> GetDonate(int id)
        {
            var donation = await _context.Donations
                .Where(d => d.DonationId == id)
                .Select(d => new DonateDetailDTO
                {
                    DonationId = d.DonationId,
                    UserId = d.UserId,
                    BloodGroup = d.BloodGroup,
                    Status = d.Status,
                    Quantity = d.Quantity,
                    DonationDate = d.DonationDate,
                    DonationTime = d.DonationTime
                })
                .FirstOrDefaultAsync();

            if (donation == null)
                throw new Exception("Không tìm thấy donation");

            return donation;
        }


        public async Task<RequestDetailDTO> GetRequest(int id)
        {
            var request = await _context.Bloodrequests
                .Where(r => r.RequestId == id)
                .Select(r => new RequestDetailDTO
                {
                    RequestId = r.RequestId,
                    UserId = r.UserId,
                    BloodGroup = r.BloodGroup,
                    Status = r.Status,
                    Quantity = r.Quantity,
                    RequestDate = r.RequestDate,
                    RequestTime = r.RequestTime
                })
                .FirstOrDefaultAsync();

            if (request == null)
                throw new Exception("Không tìm thấy yêu cầu");

            return request;
        }


        public async Task<IEnumerable<DonationViewDTO>> GetAllDonationsForAdmin()
        {
            return await _context.Donations
                .Select(d => new DonationViewDTO
                {
                    BloodGroup = d.BloodGroup,
                    Status = d.Status,
                    Quantity = d.Quantity,
                    DonationDate = d.DonationDate,
                    DonationTime = d.DonationTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DonationViewDTO>> SearchDonations(string? bloodGroup, string? status)
        {
            var query = _context.Donations.AsQueryable();

            if (!string.IsNullOrEmpty(bloodGroup))
                query = query.Where(d => d.BloodGroup == bloodGroup);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(d => d.Status == status);

            return await query
                .Select(d => new DonationViewDTO
                {
                    BloodGroup = d.BloodGroup,
                    Status = d.Status,
                    Quantity = d.Quantity,
                    DonationDate = d.DonationDate,
                    DonationTime = d.DonationTime
                })
                .ToListAsync();
        }




        public async Task<IEnumerable<RequestsViewDTO>> GetAllRequestsForAdmin()
        {
            return await _context.Bloodrequests
                .Select(r => new RequestsViewDTO
                {

                    BloodGroup = r.BloodGroup,
                    Status = r.Status,
                    Quantity = r.Quantity,
                    RequestDate = r.RequestDate,
                    RequestTime = r.RequestTime
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<RequestsViewDTO>> SearchRequests(string? bloodGroup, string? status)
        {
            var query = _context.Bloodrequests.AsQueryable();

            if (!string.IsNullOrEmpty(bloodGroup))
                query = query.Where(r => r.BloodGroup == bloodGroup);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status == status);

            return await query
                .Select(r => new RequestsViewDTO
                {
                    BloodGroup = r.BloodGroup,
                    Status = r.Status,
                    Quantity = r.Quantity,
                    RequestDate = r.RequestDate,
                    RequestTime = r.RequestTime
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateDonationStatusAsync(int donationId, string newStatus)
        {
            var donation = await _context.Donations.FindAsync(donationId);
            if (donation == null) return false;

            donation.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateRequestStatusAsync(int requestId, string newStatus)
        {
            var bloodRequest = await _context.Bloodrequests.FindAsync(requestId);
            if (bloodRequest == null) return false;

            bloodRequest.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
