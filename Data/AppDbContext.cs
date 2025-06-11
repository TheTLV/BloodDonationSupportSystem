using BloodDonationSupportSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<User> Users { get; set; }

    }
}
