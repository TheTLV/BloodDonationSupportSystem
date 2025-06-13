using Microsoft.AspNetCore.Mvc;
using BloodDonationSupportSystem.Services;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BloodDonationSupportSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await _userService.GetAllUsersAsync());

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDTO dto) =>
            Ok(await _userService.UpdateUserAsync(id, dto));

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id) =>
            Ok(await _userService.DeleteUserAsync(id));
    }

}
