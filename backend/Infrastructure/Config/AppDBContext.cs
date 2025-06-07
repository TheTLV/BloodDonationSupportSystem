using System;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<BloodRequest> BloodRequests { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Blog> Blogs { get; set; }

}
