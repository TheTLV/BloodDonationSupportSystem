using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class BloodService : IBloodService
    {
        private readonly IDonationRepository _donationRepo;
        private readonly IBloodRequestRepository _requestRepo;

        public BloodService(IDonationRepository donationRepo, IBloodRequestRepository requestRepo)
        {
            _donationRepo = donationRepo;
            _requestRepo = requestRepo;
        }

        public async Task<bool> CreateRequestAsync(int userId, BloodRequestDTO dto)
        {
            var request = new Bloodrequest
            {
                UserId = userId,
                BloodGroup = dto.BloodType,
                Quantity = dto.Quantity,
                RequestDate = dto.RequestDate,
                RequestTime = dto.RequestTime,
                Status = "Chờ duyệt"
            };

            _requestRepo.Add(request);
            return await _requestRepo.SaveChangesAsync();
        }

        public async Task<bool> CreateDonationAsync(int userId ,BloodDonationDTO dto)
        {
            var donation = new Donation
            {
                UserId = userId,
                BloodGroup = dto.BloodType,
                Quantity = dto.Quantity,
                DonationDate = dto.DonationDate,
                DonationTime = dto.DonationTime,
                Status = "Chờ duyệt",
                Height = dto.Height,
                Weight = dto.Weight,
                ChronicDisease = dto.ChronicDisease,
                Medication = dto.Medication,
                LastDonationDate = dto.LastDonationDate
            };

            _donationRepo.Add(donation);
            return await _donationRepo.SaveChangesAsync();
        }

        public IEnumerable<DonationViewDTO> GetDonationsByUserId(int userId)
        {
            return _donationRepo.GetAll()
                .Where(d => d.UserId == userId)
                .Select(d => new DonationViewDTO
                {
                    Id = d.DonationId,
                    BloodGroup = d.BloodGroup,
                    Status = d.Status,
                    Quantity = d.Quantity,
                    DonationDate = d.DonationDate,
                    DonationTime = d.DonationTime
                })
                .ToList();
        }

        public IEnumerable<RequestsViewDTO> GetRequestsByUserId(int userId)
        {
            return _requestRepo.GetAll()
                .Where(r => r.UserId == userId)
                .Select(r => new RequestsViewDTO
                {
                    Id = r.RequestId,
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
            var donation = await _donationRepo.GetAll()
                .Where(d => d.DonationId == id)
                .Select(d => new DonateDetailDTO
                {
                    DonationId = d.DonationId,
                    UserId = d.UserId,
                    BloodGroup = d.BloodGroup,
                    Status = d.Status,
                    Quantity = d.Quantity,
                    DonationDate = d.DonationDate,
                    DonationTime = d.DonationTime,
                    Height = d.Height,
                    Weight = d.Weight,
                    ChronicDisease = d.ChronicDisease,
                    Medication = d.Medication
                })
                .FirstOrDefaultAsync();

            if (donation == null)
                throw new Exception("Không tìm thấy donation");

            return donation;
        }


        public async Task<RequestDetailDTO> GetRequest(int id)
        {
            var request = await _requestRepo.GetAll()
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


        public async Task<IEnumerable<AdminDonationViewAllDTO>> GetAllDonationsForAdmin()
        {
            return await _donationRepo.GetAll()
                .Include(d => d.User)
                .Select(d => new AdminDonationViewAllDTO
                {
                    DonationId = d.DonationId,
                    UserId = d.UserId,
                    Fullname = d.User.Fullname,
                    Status = d.Status,
                    BloodGroup = d.BloodGroup,
                    Quantity = d.Quantity,
                    Gender = d.User.Profile.Gender,
                    DateOfBirth = d.User.Profile.DateOfBirth,
                    DonationDate = d.DonationDate,
                    DonationTime = d.DonationTime,
                    Height = d.Height,
                    Weight = d.Weight,
                    ChronicDisease = d.ChronicDisease,
                    Medication = d.Medication
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<DonationViewDTO>> SearchDonations(string? bloodGroup, string? status)
        {
            var query = _donationRepo.GetAll();

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


        public async Task<IEnumerable<AdminRequestsViewAllDTO>> GetAllRequestsForAdmin()
        {
            return await _requestRepo.GetAll()
                .Include(r => r.User)
                    .ThenInclude(u => u.Profile)
                .Select(r => new AdminRequestsViewAllDTO
                {
                    RequestId = r.RequestId,
                    UserId = r.UserId,
                    Fullname = r.User.Fullname,
                    Status = r.Status,
                    BloodGroup = r.BloodGroup,
                    Quantity = r.Quantity,
                    Gender = r.User.Profile.Gender,
                    DateOfBirth = r.User.Profile.DateOfBirth,
                    RequestDate = r.RequestDate,
                    RequestTime = r.RequestTime
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<RequestsViewDTO>> SearchRequests(string? bloodGroup, string? status)
        {
            var query = _requestRepo.GetAll();

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
            var donation = await _donationRepo.GetByIdAsync(donationId);
            if (donation == null) return false;

            donation.Status = newStatus;
            return await _donationRepo.SaveChangesAsync();
        }


        public async Task<bool> UpdateRequestStatusAsync(int requestId, string newStatus)
        {
            var bloodRequest = await _requestRepo.GetByIdAsync(requestId);
            if (bloodRequest == null) return false;

            bloodRequest.Status = newStatus;
            return await _requestRepo.SaveChangesAsync();
        }
    }
}
