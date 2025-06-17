using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(UserService userService , JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
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
                if (user == null)
                    return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu" });

                var token = _jwtService.GenerateToken(user);

                return Ok(new
                {
                    message = "Đăng nhập thành công",
                    token,
                    user = new
                    {
                        user.UserId,
                        user.Fullname,
                        user.Email,
                        user.PhoneNumber,
                        Role = user.RId
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

