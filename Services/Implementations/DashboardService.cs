using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs.DashboardDTOs;
using BloodDonationSupportSystem.DTOs.DashboardDTOs.BloodDonationSupportSystem.DTOs.DashboardDTOs;
using BloodDonationSupportSystem.Services.Interfaces;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public DashboardSummaryDTO GetSummary()
        {
            return new DashboardSummaryDTO
            {
                TotalUsers = _context.Users.Count(),
                TotalEvents = _context.Events.Count(),
                TotalDonations = _context.Donations.Count(),
                TotalRequests = _context.Bloodrequests.Count()
            };
        }

        public List<MonthlyDonationDTO> GetMonthlyDonationStats()
        {
            return _context.Donations
                .GroupBy(d => d.DonationDate.Month)
                .Select(g => new MonthlyDonationDTO
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .OrderBy(m => m.Month)
                .ToList();
        }
    }
}
