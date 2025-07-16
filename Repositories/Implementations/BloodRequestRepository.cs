using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        private readonly AppDbContext _context;
        public BloodRequestRepository(AppDbContext context) => _context = context;

        public void Add(Bloodrequest request) => _context.Bloodrequests.Add(request);
        public async Task<Bloodrequest?> GetByIdAsync(int id) => await _context.Bloodrequests.FindAsync(id);
        public IQueryable<Bloodrequest> GetAll() => _context.Bloodrequests.AsQueryable();
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
