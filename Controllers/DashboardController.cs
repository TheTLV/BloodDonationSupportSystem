using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "2,3")]  
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var summary = _dashboardService.GetSummary();
            return Ok(summary);
        }

        [HttpGet("monthly-donations")]
        public IActionResult GetMonthlyDonations()
        {
            var stats = _dashboardService.GetMonthlyDonationStats();
            return Ok(stats);
        }
    }
}
