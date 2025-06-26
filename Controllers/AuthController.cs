using System.Net;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using BloodDonationSupportSystem.DTOs.AuthDTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Implementations;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(IAuthService authService , JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO dto)
        {
            try
            {
                var user = _authService.Register(
                    dto.Name,
                    dto.Email,
                    dto.Password,
                    dto.PhoneNumber,
                    dto.Gender,
                    dto.DateOfBirth,
                    dto.Address
                );

                var token = _jwtService.GenerateToken(user); 

                return Ok(new
                {
                    message = "Đăng ký thành công",
                    token,
                    user = new
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
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO dto)
        {
            try
            {
                var user = _authService.Login(dto.Email, dto.Password);
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
                        Role = user.RoleId
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

