using System.Net;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
        {
            try
            {
                var user = await _authService.Register(dto);
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
                        Role= 1
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var user = await _authService.Login(dto);
            var token = _jwtService.GenerateToken(user);

            var result = new LoginResultDTO
            {
                UserId = user.UserId,
                Name = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role?.RoleName ?? "Unknown",
                Token = token
            };

            return Ok(result);
        }




    }
}

