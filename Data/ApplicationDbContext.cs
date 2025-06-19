using Microsoft.EntityFrameworkCore;
using BloodDonationSupportSystem.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection.Metadata;

namespace BloodDonationSupportSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AutoMapper.Profile> Profiles { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints here
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Models.Profile>(p => p.UserId);
        }
    }
}