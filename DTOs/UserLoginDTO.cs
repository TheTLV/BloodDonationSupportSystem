using System.ComponentModel.DataAnnotations;

namespace BloodDonationSupportSystem.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Phải nhập mật khẩu")]
        public required string Password { get; set; } 
    }
}
