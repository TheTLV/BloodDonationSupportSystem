using System;

using Microsoft.AspNetCore.Mvc;
using backend.DTO;
using backend.Models;
using backend.Interface;
using BCrypt.Net;

[ApiController]
[Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public AccountController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO request)
        {
            var existingUser = _userRepo.GetByEmail(request.Email);
            if (existingUser != null)
                return BadRequest("Email đã tồn tại.");

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Fullname = request.Fullname,
                RoleId = 2 // member
            };

            _userRepo.Create(newUser);
            return Ok("Đăng ký thành công.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            var user = _userRepo.GetByEmail(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return Unauthorized("Email hoặc mật khẩu không đúng.");

            return Ok(new
            {
                Message = "Đăng nhập thành công.",
                UserId = user.UserId,
                Email = user.Email,
                Fullname = user.Fullname,
                RoleId = user.RoleId
            });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDTO request)
        {
            var user = _userRepo.GetByEmail(request.Email);
            if (user == null)
                return NotFound("Email không tồn tại.");

            // Giả lập xử lý (gửi email, tạo mã reset,...)
            return Ok("Mã khôi phục mật khẩu đã được gửi qua email.");
        }
    }


