using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Services.Implementations;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "2,3")] // Admin role
    public class AdminController : ControllerBase
    {
        private readonly IBloodService _bloodService;
        private readonly IUserService _userService;

        public AdminController(IBloodService bloodService, IUserService userService)
        {
            _bloodService = bloodService;
            _userService = userService;
        }

        // ======== DONATION ========

        [HttpGet("donations")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> GetAllDonations()
        {
            var result = await _bloodService.GetAllDonationsForAdmin();
            return Ok(result);
        }

        [HttpGet("donations/search")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> SearchDonations([FromQuery] string? bloodGroup, [FromQuery] string? status)
        {
            var result = await _bloodService.SearchDonations(bloodGroup, status);
            return Ok(result);
        }

        [HttpPut("donations/{id}/status")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> UpdateDonationStatus(int id, [FromBody] string newStatus)
        {
            var success = await _bloodService.UpdateDonationStatusAsync(id, newStatus);
            if (!success) return NotFound(new { message = "Không tìm thấy donation" });
            return Ok(new { message = "Cập nhật trạng thái donation thành công" });
        }

        // ======== REQUEST ========

        [HttpGet("requests")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> GetAllRequests()
        {
            var result = await _bloodService.GetAllRequestsForAdmin();
            return Ok(result);
        }

        [HttpGet("requests/search")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> SearchRequests([FromQuery] string? bloodGroup, [FromQuery] string? status)
        {
            var result = await _bloodService.SearchRequests(bloodGroup, status);
            return Ok(result);
        }

        [HttpPut("requests/{id}/status")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> UpdateRequestStatus(int id, [FromBody] string newStatus)
        {
            var success = await _bloodService.UpdateRequestStatusAsync(id, newStatus);
            if (!success) return NotFound(new { message = "Không tìm thấy yêu cầu" });
            return Ok(new { message = "Cập nhật trạng thái yêu cầu thành công" });
        }

        // ======== USER MANAGEMENT ========

        [HttpGet("users")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("users/{id}/status")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] int statusId)
        {
            var result = await _userService.UpdateUserStatusAsync(id, statusId);
            if (!result) return NotFound(new { message = "Không tìm thấy user" });
            return Ok(new { message = "Cập nhật trạng thái user thành công" });
        }

        [HttpPut("users/{id}/role")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] int newRoleId)
        {
            var success = await _userService.UpdateUserRoleAsync(id, newRoleId);
            if (!success) return NotFound(new { message = "Không tìm thấy người dùng" });
            return Ok(new { message = "Cập nhật quyền người dùng thành công" });
        }

        [HttpGet("user/{id}")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> GetUserDetailById(int id)
        {
            var users = await _userService.GetUserDetailByIdAsync(id);
            return Ok(users);
        }
    }
}
