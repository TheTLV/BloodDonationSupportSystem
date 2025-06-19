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
        private readonly IBloodService _bloodService;

        public UserController(
            IUserService userService,
            IBloodService bloodService)
        {
            _userService = userService;
            _bloodService = bloodService;
        }

        [HttpPost("donate")]
        public IActionResult Donate([FromBody] BloodDonationDTO dto)
        {
            var success = _bloodService.CreateDonation(dto);
            if (!success) return BadRequest(new { message = "Lỗi tạo yêu cầu hiến máu" });
            return Ok(new { message = "Gửi yêu cầu hiến máu thành công" });
        }

        [HttpPost("request")]
        public IActionResult RequestBlood([FromBody] BloodRequestDTO dto)
        {
            var success = _bloodService.CreateRequest(dto);
            if (!success) return BadRequest(new { message = "Lỗi gửi yêu cầu xin máu" });
            return Ok(new { message = "Gửi yêu cầu xin máu thành công" });
        }

        [HttpGet("donations")]
        public IActionResult GetDonations()
        {
            var userId = GetUserIdFromToken();
            var donations = _bloodService.GetDonationsByUserId(userId);
            return Ok(donations);
        }

        [HttpGet("requests")]
        public IActionResult GetRequests()
        {
            var userId = GetUserIdFromToken();
            var requests = _bloodService.GetRequestsByUserId(userId);
            return Ok(requests);
        }
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                throw new Exception("Không tìm thấy UserId trong token");

            return int.Parse(userIdClaim.Value);
        }


    }
}
