using Microsoft.AspNetCore.Mvc;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BloodDonationSupportSystem.DTOs;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "3")] 
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        //[HttpGet("users")]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var result = await _userService.GetAllUsersAsync();
        //    return Ok(result);
        //}

        //[HttpPut("users/{id}")]
        //public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDTO dto)
        //{
        //    var result = await _userService.UpdateUserAsync(id, dto);
        //    return Ok(result);
        //}

        //[HttpDelete("users/{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var result = await _userService.DeleteUserAsync(id);
        //    return Ok(result);
        //}
    }
}
