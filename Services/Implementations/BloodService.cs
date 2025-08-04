using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.BloodDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class BloodService : IBloodService
    {
        private readonly IDonationRepository _donationRepo;
        private readonly IBloodRequestRepository _requestRepo;
        private readonly IDonationEligibilityRepository _eligibilityRepo;
        private readonly IBloodBankRepository _bloodBankRepo;
        private readonly IProfileRepository _profileRepo;


        public BloodService(
            IDonationRepository donationRepo,
            IBloodRequestRepository requestRepo,
            IDonationEligibilityRepository eligibilityRepo,
            IBloodBankRepository bloodBankRepo,
            IProfileRepository profileRepo)
        {
            _donationRepo = donationRepo;
            _requestRepo = requestRepo;
            _eligibilityRepo = eligibilityRepo;
            _bloodBankRepo = bloodBankRepo;
            _profileRepo = profileRepo;
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

        public async Task<bool> CreateDonationAsync(int userId, BloodDonationDTO dto)
        {
            var donation = new Donation
            {
                UserId = userId,
                Quantity = dto.Quantity,
                DonationDate = dto.DonationDate,
                DonationTime = dto.DonationTime,
                Status = "Đang chờ duyệt",
                Height = dto.Height,
                Weight = dto.Weight,
                ChronicDisease = dto.ChronicDisease,
                Medication = dto.Medication,
                LastDonationDate = dto.LastDonationDate
            };

            _donationRepo.Add(donation);
            var saved = await _donationRepo.SaveChangesAsync();

            if (saved)
            {
                var eligibility = new DonationEligibility
                {
                    UserId = userId,
                    DonationId = donation.DonationId,
                    ScreeningDate = DateOnly.FromDateTime(DateTime.Now),
                    Notes = "Chờ khám sàng lọc y tế"
                };
                await _eligibilityRepo.AddAsync(eligibility);
            }

            return saved;
        }


        public IEnumerable<DonationViewDTO> GetDonationsByUserId(int userId)
        {
            return _donationRepo.GetAll()
                .Where(d => d.UserId == userId)
                .Select(d => new DonationViewDTO
                {
                    Id = d.DonationId,
                    BloodGroup = d.User.Profile.BloodGroup,
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

        //public async Task<DonateDetailDTO> GetDonate(int id)
        //{
        //    var donation = await _donationRepo.GetAll()
        //        .Where(d => d.DonationId == id)
        //        .Select(d => new DonateDetailDTO
        //        {
        //            DonationId = d.DonationId,
        //            UserId = d.UserId,
        //            BloodGroup = d.User.Profile.BloodGroup,
        //            Status = d.Status,
        //            Quantity = d.Quantity,
        //            DonationDate = d.DonationDate,
        //            DonationTime = d.DonationTime,
        //            Height = d.Height,
        //            Weight = d.Weight,
        //            ChronicDisease = d.ChronicDisease,
        //            Medication = d.Medication
        //        })
        //        .FirstOrDefaultAsync();

        //    if (donation == null)
        //        throw new Exception("Không tìm thấy donation");

        //    return donation;
        //}

        public async Task<DonateDetailDTO> GetDonate(int donationId)
        {
            // Lấy thông tin donation + profile + user
            var donation = await _donationRepo.GetAll()
                .Where(d => d.DonationId == donationId)
                .Select(d => new
                {
                    d.DonationId,
                    d.Quantity,
                    d.DonationDate,
                    d.DonationTime,
                    d.Height,
                    d.Weight,
                    d.Status,
                    BloodGroup = d.User.Profile.BloodGroup,
                    Eligibilities = d.DonationEligibilities
                })
                .FirstOrDefaultAsync();

            if (donation == null)
                throw new Exception("Không tìm thấy donation");

            // Tìm bản ghi sàng lọc y tế mới nhất
            var latestEligibility = donation.Eligibilities?
                .OrderByDescending(e => e.ScreeningDate)
                .FirstOrDefault();

            return new DonateDetailDTO
            {
                DonationId = donation.DonationId,
                BloodGroup = donation.BloodGroup,
                Status = donation.Status,
                Quantity = donation.Quantity,
                DonationDate = donation.DonationDate,
                DonationTime = donation.DonationTime,
                Height = donation.Height,
                Weight = donation.Weight,

                ScreeningDate = latestEligibility?.ScreeningDate ?? default,
                HasChronicDisease = latestEligibility?.HasChronicDisease,
                HasRecentMedication = latestEligibility?.HasRecentMedication,
                HivTestResult = latestEligibility?.HivTestResult,
                HepatitisB = latestEligibility?.HepatitisB,
                HepatitisC = latestEligibility?.HepatitisC,
                Syphilis = latestEligibility?.Syphilis,
                BloodPressure = latestEligibility?.BloodPressure,
                HemoglobinLevel = latestEligibility?.HemoglobinLevel,
                Notes = latestEligibility?.Notes,
                TemperatureC = latestEligibility?.TemperatureC,
                HeartRateBpm = latestEligibility?.HeartRateBpm,
                CurrentHealthStatus = latestEligibility?.CurrentHealthStatus,
                MedicalHistory = latestEligibility?.MedicalHistory
            };
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
                    BloodGroup = d.User.Profile.BloodGroup,
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
                query = query.Where(d => d.User.Profile.BloodGroup == bloodGroup);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(d => d.Status == status);

            return await query
                .Select(d => new DonationViewDTO
                {
                    BloodGroup = d.User.Profile.BloodGroup,
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






        //doctor


        public async Task<(bool Success, string? Error)> UpdateDonationStatusAsync(int donationId, string newStatus)
        {
            var donation = await _donationRepo.GetByIdAsync(donationId);
            if (donation == null)
                return (false, "Donation không tồn tại.");

            donation.Status = newStatus.Trim();
            var saved = await _donationRepo.SaveChangesAsync();
            if (!saved)
                return (false, "Cập nhật thất bại.");

            return (true, null);
        }


        public async Task<bool> ProcessBloodQualityCheckAsync(int donationId, bool qualityPassed)
        {
            var donation = await _donationRepo.GetByIdAsync(donationId);
            if (donation == null || donation.Status != "Chờ kiểm tra máu")
                return false;

            donation.Status = qualityPassed ? "Đã nhập kho" : "Từ chối";
            await _donationRepo.UpdateAsync(donation);

            if (qualityPassed)
            {
                var existingBank = await _bloodBankRepo.GetByBloodGroupAsync(donation.User.Profile.BloodGroup);
                if (existingBank == null)
                {
                    var newBank = new BloodBank
                    {
                        BloodGroup = donation.User.Profile.BloodGroup,
                        QuantityMl = donation.Quantity ?? 0,
                        LastUpdated = DateOnly.FromDateTime(DateTime.UtcNow)
                    };
                    await _bloodBankRepo.AddAsync(newBank);
                }
                else
                {
                    await _bloodBankRepo.TryAdjustQuantityAsync(donation.User.Profile.BloodGroup, donation.Quantity ?? 0);
                }
            }

            return true;
        }


        public async Task<bool> UpdateRequestStatusAsync(int requestId, string newStatus)
        {
            var bloodRequest = await _requestRepo.GetByIdAsync(requestId);
            if (bloodRequest == null) return false;

            bloodRequest.Status = newStatus;
            await _requestRepo.UpdateAsync(bloodRequest);
            return await _requestRepo.SaveChangesAsync();
        }


        public async Task<IEnumerable<DoctorDonationViewDTO>> GetPendingDonationsForDoctor()
        {
            var donations = await _donationRepo.GetPendingDonationsAsync();
            return donations.Select(d => new DoctorDonationViewDTO
            {
                donationId = d.DonationId,
                UserId = d.User.UserId,
                BloodGroup = d.User.Profile.BloodGroup,
                Status = d.Status,
                Quantity = d.Quantity,
                DonationDate = d.DonationDate,
                DonationTime = d.DonationTime,
                Fullname = d.User?.Fullname ?? "N/A",
                Email = d.User?.Email ?? "N/A",
                PhoneNumber = d.User?.PhoneNumber ?? "N/A",
                DateOfBirth = d.User?.Profile?.DateOfBirth,
                Gender = d.User?.Profile?.Gender ?? "N/A",
                Address = d.User?.Profile?.Address ?? "N/A",
                IsBloodGroupVerified = d.User?.Profile?.IsBloodGroupVerified
            }).ToList();
        }


        public async Task<bool> UpdateUserBloodGroupAsync(int userId, UpdateBloodTypeDTO dto)
        {
            try
            {
                await _profileRepo.UpdateBloodGroupAsync(userId, dto.BloodType);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<(bool Success, string? Error)> ProcessBloodAnalysisAsync(int donationId, PostDonationAnalysisDTO dto)
        {
            var donation = await _donationRepo.GetByIdAsync(donationId);
            if (donation == null)
                return (false, "Donation không tồn tại.");

            var existing = await _eligibilityRepo.GetByDonationIdAsync(donationId);
            var eligibilityRecord = existing.FirstOrDefault();
            if (eligibilityRecord == null)
                return (false, "Không tìm thấy thông tin sàng lọc.");

            // Cập nhật thông tin form sau khi lấy máu
            eligibilityRecord.HasChronicDisease = dto.HasChronicDisease;
            eligibilityRecord.HasRecentMedication = dto.HasRecentMedication;
            eligibilityRecord.HivTestResult = dto.HivTestResult;
            eligibilityRecord.HepatitisB = dto.HepatitisB;
            eligibilityRecord.HepatitisC = dto.HepatitisC;
            eligibilityRecord.Syphilis = dto.Syphilis;
            eligibilityRecord.HemoglobinLevel = dto.HemoglobinLevel;
            eligibilityRecord.Notes = dto.Notes;

            await _eligibilityRepo.UpdateAsync(eligibilityRecord);

            donation.Status = "Đã nhập kho";
            await _donationRepo.UpdateAsync(donation);

            return (true, null);
        }

        public async Task<(bool Success, string? Error)> ProcessMedicalScreeningAsync(int donationId, PreDonationScreeningDTO dto)
        {
            var donation = await _donationRepo.GetByIdAsync(donationId);
            if (donation == null)
                return (false, "Donation không tồn tại.");

            var existing = await _eligibilityRepo.GetByDonationIdAsync(donationId);
            var eligibilityRecord = existing.FirstOrDefault();
            if (eligibilityRecord == null)
            {
                eligibilityRecord = new DonationEligibility
                {
                    UserId = donation.UserId ?? 0,
                    DonationId = donationId,
                    ScreeningDate = DateOnly.FromDateTime(DateTime.UtcNow)
                };
                await _eligibilityRepo.AddAsync(eligibilityRecord);
            }

            // Cập nhật thông tin form trước khi lấy máu
            eligibilityRecord.TemperatureC = dto.TemperatureC;
            eligibilityRecord.HeartRateBpm = dto.HeartRateBpm;
            eligibilityRecord.CurrentHealthStatus = dto.CurrentHealthStatus;
            eligibilityRecord.BloodPressure = dto.BloodPressure;
            eligibilityRecord.MedicalHistory = dto.MedicalHistory;

            await _eligibilityRepo.UpdateAsync(eligibilityRecord);

            await _donationRepo.UpdateAsync(donation);

            return (true, null);
        }



        public async Task<DoctorNoteDTO> DoctorNoteAsync(int donationId, DoctorNoteDTO note)
        {
            var donation = await _donationRepo.GetByIdAsync(donationId);
            if (donation == null)
                throw new Exception("Không tìm thấy donation");

            var eligibilityList = await _eligibilityRepo.GetByDonationIdAsync(donationId);
            var latestEligibility = eligibilityList
                .OrderByDescending(e => e.ScreeningDate)
                .FirstOrDefault();

            if (latestEligibility == null)
                throw new Exception("Không tìm thấy thông tin sàng lọc y tế");

            // Cập nhật ghi chú của bác sĩ
            latestEligibility.Notes = note.Notes;
            await _eligibilityRepo.UpdateAsync(latestEligibility);
            await _eligibilityRepo.SaveChangesAsync();

            return new DoctorNoteDTO
            {
                Notes = latestEligibility.Notes,
            };
        }


        public async Task<IEnumerable<DoctorNoteDTO>> GetDoctorNotesAsync(int donationId)
        {
            var eligibilities = await _eligibilityRepo.GetByDonationIdAsync(donationId);

            return eligibilities.Select(e => new DoctorNoteDTO
            {
                Notes = e.Notes
            });
        }








        // Blood Bank Management
        public async Task AddOrUpdateStockAsync(string bloodGroup, int quantityMl)
        {
            var entry = await _bloodBankRepo.GetByBloodGroupAsync(bloodGroup);
            if (entry != null)
            {
                entry.QuantityMl = (entry.QuantityMl ?? 0) + quantityMl;
                entry.LastUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
                await _bloodBankRepo.UpdateAsync(entry);
            }
            else
            {
                var newEntry = new BloodBank
                {
                    BloodGroup = bloodGroup,
                    QuantityMl = quantityMl,
                    LastUpdated = DateOnly.FromDateTime(DateTime.UtcNow)
                };
                await _bloodBankRepo.AddAsync(newEntry);
            }

        }
        

        public Task<BloodBank?> GetStockByGroupAsync(string bloodGroup)
            => _bloodBankRepo.GetByBloodGroupAsync(bloodGroup);

        public async Task<IEnumerable<BloodBank>> GetAllStocksAsync()
        {
            return await _bloodBankRepo.GetAllAsync();
        }


    }
}