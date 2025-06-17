using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "1,2,3")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBloodRequestService _bloodRequestService;
        private readonly IDonationService _donationService;

        public UserController(
            IUserService userService,
            IBloodRequestService bloodRequestService,
            IDonationService donationService)
        {
            _userService = userService;
            _bloodRequestService = bloodRequestService;
            _donationService = donationService;
        }

        [HttpGet]
        [HttpPost("donate")]
        public IActionResult Donate([FromBody] BloodDonationDTO dto)
        {
            var success = _donationService.CreateDonation(dto);
            if (!success) return BadRequest(new { message = "Lỗi tạo yêu cầu hiến máu" });
            return Ok(new { message = "Gửi yêu cầu hiến máu thành công" });
        }

        [HttpPost("request")]
        public IActionResult RequestBlood([FromBody] BloodRequestDTO dto)
        {
            var success = _bloodRequestService.CreateRequest(dto);
            if (!success) return BadRequest(new { message = "Lỗi gửi yêu cầu xin máu" });
            return Ok(new { message = "Gửi yêu cầu xin máu thành công" });
        }

        [HttpGet("donations")]
        public IActionResult GetDonations([FromQuery] int userId)
        {
            var donations = _donationService.GetByUserId(userId);
            return Ok(donations);
        }

        [HttpGet("requests")]
        public IActionResult GetRequests([FromQuery] int userId)
        {
            var requests = _bloodRequestService.GetByUserId(userId);
            return Ok(requests);
        }


    }
}
