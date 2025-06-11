using BloodDonationSupportSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controller
{
    public class AuthController 
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO dto)
        {
            var user = _userService.Register(dto.Name, dto.Email, dto.Password, dto.Phone);
            if (user == null) return BadRequest("Email already exists!");
            return Ok(user);
        }

    }
}
