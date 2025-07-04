﻿using System.ComponentModel.DataAnnotations;

namespace BloodDonationSupportSystem.DTOs.AuthDTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phải nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public required string Password { get; set; } 
    }
}
