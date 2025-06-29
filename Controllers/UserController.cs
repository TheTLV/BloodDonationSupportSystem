using BloodDonationSupportSystem.DTOs.BloodDTO;
using BloodDonationSupportSystem.DTOs.UsersDTOs;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "1,2,3")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBloodService _bloodService;
        private int userId => GetUserIdFromToken();
        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                throw new Exception("Không tìm thấy UserId trong token");

            return int.Parse(userIdClaim.Value);
        }
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

            var success = _bloodService.CreateDonation(userId, dto);
            if (!success) return BadRequest(new { message = "Lỗi tạo yêu cầu hiến máu" });
            return Ok(new { message = "Gửi yêu cầu hiến máu thành công" });
        }

        [HttpPost("request")]
        public IActionResult RequestBlood([FromBody] BloodRequestDTO dto)
        {
            var success = _bloodService.CreateRequest(userId, dto);
            if (!success) return BadRequest(new { message = "Lỗi gửi yêu cầu xin máu" });
            return Ok(new { message = "Gửi yêu cầu xin máu thành công" });
        }

        [HttpGet("donations")]
        public IActionResult GetDonations()
        {
            var donations = _bloodService.GetDonationsByUserId(userId);
            return Ok(donations);
        }

        [HttpGet("requests")]
        public IActionResult GetRequests()
        {
            var requests = _bloodService.GetRequestsByUserId(userId);
            return Ok(requests);
        }



        [HttpGet("profile")]
        public async Task<IActionResult> GetOwnProfile()
        {
            var userDetail = await _userService.GetOwnProfileAsync(userId);
            if (userDetail == null) return NotFound(new { message = "Không tìm thấy thông tin người dùng" });
            return Ok(userDetail);
        }

        [HttpPut("updateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateDTO dto)
        {
            var updatedUser = await _userService.UpdateMyProfileAsync(userId, dto);
            if (updatedUser == null) return BadRequest(new { message = "Cập nhật thông tin người dùng thất bại" });
            return Ok(updatedUser);
        }


        [HttpPut("updateDonation/{id}")]
        public IActionResult UpdateMyDonation(int id, [FromBody] DonationUpdateDTO dto)
        {
            try
            {
                _userService.UpdateMyDonation(id, dto, userId); // Truyền ID riêng
                return Ok(new { message = "Cập nhật yêu cầu hiến máu thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("deleteRequest/{id}")]
        public async Task<IActionResult> DeleteMyRequest(int id)
        {
            var success = await _userService.DeleteMyBloodRequestAsync(id, userId);
            if (!success) return NotFound(new { message = "Không tìm thấy yêu cầu" });
            return Ok(new { message = "Đã hủy yêu cầu thành công" });
        }

        [HttpDelete("deleteDonation/{id}")]
        public async Task<IActionResult> DeleteMyDonation(int id)
        {
           
            var success = await _userService.DeleteMyDonationAsync(id, userId);
            if (!success) return NotFound(new { message = "Không tìm thấy hiến máu" });
            return Ok(new { message = "Đã hủy hiến máu thành công" });
        }


        [HttpPut("updateRequest/{id}")]
        public IActionResult UpdateMyBloodRequest([FromBody] RequestUpdateDTO dto)
        {
            try
            {
                _userService.UpdateMyBloodRequest(dto, userId);
                return Ok(new { message = "Cập nhật yêu cầu xin máu thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getDonate/{id}")]
        public async Task<IActionResult> GetDonate(int id)
        {
            try
            {
                var result = await _bloodService.GetDonate(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("getRequest/{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            try
            {
                var result = await _bloodService.GetRequest(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }





    }
}
