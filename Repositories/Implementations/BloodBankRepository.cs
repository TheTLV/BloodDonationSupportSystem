using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class BloodBankRepository : IBloodBankRepository
    {
        private readonly AppDbContext _context;

        public BloodBankRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BloodBank?> GetByIdAsync(int bankId)
        {
            return await _context.BloodBanks.FindAsync(bankId);
        }

        public async Task<IEnumerable<BloodBank>> GetAllAsync()
        {
            return await _context.BloodBanks.AsNoTracking().ToListAsync();
        }

        public async Task<BloodBank?> GetByBloodGroupAsync(string bloodGroup)
        {
            return await _context.BloodBanks
                .FirstOrDefaultAsync(b => b.BloodGroup == bloodGroup);
        }


        public async Task<bool> HasMinimumQuantityAsync(string bloodGroup, int minQuantityMl)
        {
            var entry = await _context.BloodBanks
                .FirstOrDefaultAsync(b => b.BloodGroup == bloodGroup);
            return entry != null && entry.QuantityMl >= minQuantityMl;
        }

        public async Task AddAsync(BloodBank bloodBank)
        {
            await _context.BloodBanks.AddAsync(bloodBank);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BloodBank bloodBank)
        {
            _context.BloodBanks.Update(bloodBank);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int bankId)
        {
            var entity = await _context.BloodBanks.FindAsync(bankId);
            if (entity != null)
            {
                _context.BloodBanks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TryAdjustQuantityAsync(string bloodGroup, int deltaMl)
        {
            var entry = await _context.BloodBanks
                .FirstOrDefaultAsync(b => b.BloodGroup == bloodGroup);

            if (entry == null) return false;

            entry.QuantityMl += deltaMl;
            entry.LastUpdated = DateOnly.FromDateTime(DateTime.UtcNow);
            _context.BloodBanks.Update(entry);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
