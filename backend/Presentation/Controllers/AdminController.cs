using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodDonationSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // UC-22: Manage Users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        // UC-22: Manage Users
        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // UC-22: Manage Users
        [HttpPost("users")]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var createdUser = await _adminService.CreateUserAsync(userCreateDto);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // UC-22: Manage Users
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (id != userUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await _adminService.UpdateUserAsync(userUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-22: Manage Users
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _adminService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-23: Manage Staff
        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaff()
        {
            var staff = await _adminService.GetAllStaffAsync();
            return Ok(staff);
        }

        // UC-23: Manage Staff
        [HttpGet("staff/{id}")]
        public async Task<ActionResult<StaffDto>> GetStaffMember(int id)
        {
            var staff = await _adminService.GetStaffByIdAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        // UC-23: Manage Staff
        [HttpPost("staff")]
        public async Task<ActionResult<StaffDto>> CreateStaff([FromBody] StaffCreateDto staffCreateDto)
        {
            var createdStaff = await _adminService.CreateStaffAsync(staffCreateDto);
            return CreatedAtAction(nameof(GetStaffMember), new { id = createdStaff.Id }, createdStaff);
        }

        // UC-23: Manage Staff
        [HttpPut("staff/{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] StaffUpdateDto staffUpdateDto)
        {
            if (id != staffUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await _adminService.UpdateStaffAsync(staffUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-23: Manage Staff
        [HttpDelete("staff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await _adminService.DeleteStaffAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-24: View Dashboard
        [HttpGet("dashboard")]
        public async Task<ActionResult<AdminDashboardDto>> GetDashboard()
        {
            var dashboard = await _adminService.GetAdminDashboardDataAsync();
            return Ok(dashboard);
        }

        // UC-25: View Feedback
        [HttpGet("feedbacks")]
        public async Task<ActionResult<IEnumerable<FeedbackDto>>> GetFeedbacks()
        {
            var feedbacks = await _adminService.GetAllFeedbacksAsync();
            return Ok(feedbacks);
        }

        // UC-37: View User List
        [HttpGet("users/list")]
        public async Task<ActionResult<IEnumerable<UserListDto>>> GetUserList()
        {
            var users = await _adminService.GetUserListAsync();
            return Ok(users);
        }

        // UC-38: Create Staff Account
        [HttpPost("staff/accounts")]
        public async Task<ActionResult<StaffAccountDto>> CreateStaffAccount([FromBody] StaffAccountCreateDto accountCreateDto)
        {
            var createdAccount = await _adminService.CreateStaffAccountAsync(accountCreateDto);
            return CreatedAtAction(nameof(GetStaffAccount), new { id = createdAccount.Id }, createdAccount);
        }

        // UC-38: Create Staff Account
        [HttpGet("staff/accounts/{id}")]
        public async Task<ActionResult<StaffAccountDto>> GetStaffAccount(int id)
        {
            var account = await _adminService.GetStaffAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // UC-39: Update Staff/User Info
        [HttpPut("staff/accounts/{id}")]
        public async Task<IActionResult> UpdateStaffAccount(int id, [FromBody] StaffAccountUpdateDto accountUpdateDto)
        {
            if (id != accountUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await _adminService.UpdateStaffAccountAsync(accountUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // UC-40: Delete or Lock User Account
        [HttpPut("users/{id}/status")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UserStatusUpdateDto statusUpdateDto)
        {
            if (id != statusUpdateDto.UserId)
            {
                return BadRequest();
            }

            var result = await _adminService.UpdateUserStatusAsync(statusUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // System Configuration
        [HttpGet("configurations")]
        public async Task<ActionResult<IEnumerable<SystemConfigurationDto>>> GetConfigurations()
        {
            var configs = await _adminService.GetAllSystemConfigurationsAsync();
            return Ok(configs);
        }

        [HttpPut("configurations/{id}")]
        public async Task<IActionResult> UpdateConfiguration(int id, [FromBody] SystemConfigurationUpdateDto configUpdateDto)
        {
            if (id != configUpdateDto.Id)
            {
                return BadRequest();
            }

            var result = await _adminService.UpdateSystemConfigurationAsync(configUpdateDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
