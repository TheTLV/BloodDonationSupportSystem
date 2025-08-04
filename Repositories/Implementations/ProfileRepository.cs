using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Repositories.Implementations
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;
        public ProfileRepository(AppDbContext context) => _context = context;

        public void AddProfile(Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
        }
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
        public async Task UpdateBloodGroupAsync(int userId, string bloodGroup)
        {
            var profile = await _context.Profiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile != null)
            {
                profile.BloodGroup = bloodGroup;
                profile.IsBloodGroupVerified = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
