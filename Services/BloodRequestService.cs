using AutoMapper;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories;

namespace BloodDonationSupportSystem.Services
{
    public class BloodRequestService
    {
        private readonly IBloodRequestRepository _bloodRequestRepository;
        private readonly IMapper _mapper;

        public BloodRequestService(IBloodRequestRepository bloodRequestRepository, IMapper mapper)
        {
            _bloodRequestRepository = bloodRequestRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BloodRequestDto>> GetAllBloodRequestsAsync()
        {
            var requests = await _bloodRequestRepository.GetAllBloodRequestsAsync();
            return _mapper.Map<IEnumerable<BloodRequestDto>>(requests);
        }

        public async Task<BloodRequestDto> GetBloodRequestByIdAsync(int id)
        {
            var request = await _bloodRequestRepository.GetBloodRequestByIdAsync(id);
            return _mapper.Map<BloodRequestDto>(request);
        }

        public async Task AddBloodRequestAsync(BloodRequest bloodRequest)
        {
            await _bloodRequestRepository.AddBloodRequestAsync(bloodRequest);
        }

        public async Task UpdateBloodRequestAsync(BloodRequest bloodRequest)
        {
            await _bloodRequestRepository.UpdateBloodRequestAsync(bloodRequest);
        }

        public async Task DeleteBloodRequestAsync(int id)
        {
            await _bloodRequestRepository.DeleteBloodRequestAsync(id);
        }

        public async Task<IEnumerable<BloodRequestDto>> GetEmergencyRequestsAsync()
        {
            var requests = await _bloodRequestRepository.GetAllBloodRequestsAsync();
            var emergencyRequests = requests.Where(r => r.Urgency == "Emergency");
            return _mapper.Map<IEnumerable<BloodRequestDto>>(emergencyRequests);
        }
    }
}