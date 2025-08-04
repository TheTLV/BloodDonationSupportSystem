using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class DonationRepository :IDonationRepository
    {
        private readonly AppDbContext _context;
        public DonationRepository(AppDbContext context) => _context = context;

        public void Add(Donation donation) => _context.Donations.Add(donation);
        public async Task<Donation?> GetByIdAsync(int id) => await _context.Donations.FindAsync(id);
        public IQueryable<Donation> GetAll() => _context.Donations.AsQueryable();
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
        public async Task UpdateAsync(Donation donation)
        {
            _context.Donations.Update(donation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Donation>> GetPendingDonationsAsync()
        {
            return await _context.Donations
                //.Where(d => d.Status == "Đã đặt lịch")
                .Include(d => d.User)
                .ThenInclude(u => u.Profile)
                .ToListAsync();
        }

    }
}
