using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class DonationEligibilityRepository : IDonationEligibilityRepository
    {
        private readonly AppDbContext _context;

        public DonationEligibilityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DonationEligibility?> GetByIdAsync(int eligibilityId)
        {
            return await _context.DonationEligibilities.FindAsync(eligibilityId);
        }

        public async Task<IEnumerable<DonationEligibility>> GetByUserIdAsync(int userId)
        {
            return await _context.DonationEligibilities
                .Where(e => e.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<DonationEligibility>> GetByDonationIdAsync(int donationId)
        {
            return await _context.DonationEligibilities
                .Where(e => e.DonationId == donationId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DonationEligibility?> GetLatestByUserIdAsync(int userId)
        {
            return await _context.DonationEligibilities
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.ScreeningDate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(DonationEligibility eligibility)
        {
            await _context.DonationEligibilities.AddAsync(eligibility);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DonationEligibility eligibility)
        {
            _context.DonationEligibilities.Update(eligibility);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(int eligibilityId)
        {
            var entity = await _context.DonationEligibilities.FindAsync(eligibilityId);
            if (entity != null)
            {
                _context.DonationEligibilities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
