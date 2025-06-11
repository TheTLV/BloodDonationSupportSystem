using BloodDonationSupportSystem.Services;
using Microsoft.AspNetCore.Mvc;

using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO dto)
        {
            var user = _userService.Register(dto.Name, dto.Email, dto.Password, dto.Phone);
            if (user == null)
            {
                return BadRequest("Email đã tồn tại!");
            }

            return Ok(new
            {
                Message = "Đăng ký thành công 🎉",
                User = new
                {
                    user.UID,
                    user.Name,
                    user.Email,
                    user.PhoneNumber,
                    user.Role
                }
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO dto)
        {
            var user = _userService.Login(dto.Email, dto.Password);
            if (user == null)
            {
                return Unauthorized("Sai email hoặc mật khẩu.");
            }

            return Ok(new
            {
                Message = "Đăng nhập thành công ✅",
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Phone,
                    user.Role
                }
            });
        }
    }
}

