using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public BloodRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BloodRequest>> GetAllBloodRequestsAsync()
        {
            return await _context.BloodRequests.Include(br => br.User).ToListAsync();
        }

        public async Task<BloodRequest> GetBloodRequestByIdAsync(int id)
        {
            return await _context.BloodRequests.Include(br => br.User)
                .FirstOrDefaultAsync(br => br.RequestId == id);
        }

        public async Task AddBloodRequestAsync(BloodRequest bloodRequest)
        {
            await _context.BloodRequests.AddAsync(bloodRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBloodRequestAsync(BloodRequest bloodRequest)
        {
            _context.BloodRequests.Update(bloodRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBloodRequestAsync(int id)
        {
            var bloodRequest = await GetBloodRequestByIdAsync(id);
            if (bloodRequest != null)
            {
                _context.BloodRequests.Remove(bloodRequest);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IBloodRequestRepository
    {
        Task<IEnumerable<BloodRequest>> GetAllBloodRequestsAsync();
        Task<BloodRequest> GetBloodRequestByIdAsync(int id);
        Task AddBloodRequestAsync(BloodRequest bloodRequest);
        Task UpdateBloodRequestAsync(BloodRequest bloodRequest);
        Task DeleteBloodRequestAsync(int id);
    }
}