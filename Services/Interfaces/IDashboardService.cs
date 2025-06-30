using BloodDonationSupportSystem.DTOs.DashboardDTOs;
using BloodDonationSupportSystem.DTOs.DashboardDTOs.BloodDonationSupportSystem.DTOs.DashboardDTOs;

namespace BloodDonationSupportSystem.Services.Interfaces
{
    public interface IDashboardService
    {
        DashboardSummaryDTO GetSummary();
        List<MonthlyDonationDTO> GetMonthlyDonationStats();
    }
}
