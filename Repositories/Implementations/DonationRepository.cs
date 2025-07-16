using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;

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
    }
}
