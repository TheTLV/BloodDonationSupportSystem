using BloodDonationSupportSystem.Services;
using Microsoft.AspNetCore.Mvc;

using BloodDonationSupportSystem.DTOs;

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
            try
            {
                var user = _userService.Register(dto.Name, dto.Email, dto.Password, dto.Phone);

                return Ok(new
                {
                    Message = "Đăng ký thành công ",
                    User = new
                    {
                        user.UserId,
                        user.Fullname,
                        user.Email,
                        user.PhoneNumber,
                        user.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO dto)
        {
            try
            {
                var user = _userService.Login(dto.Email, dto.Password);

                return Ok(new
                {
                    Message = "Đăng nhập thành công ",
                    User = new
                    {
                        user.UserId,
                        user.Fullname,
                        user.Email,
                        user.PhoneNumber,
                        user.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }
    }
}

