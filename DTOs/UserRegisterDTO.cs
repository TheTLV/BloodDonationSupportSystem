using System.ComponentModel.DataAnnotations;

namespace BloodDonationSupportSystem.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phải nhập mật khẩu")]
        public required string Password { get; set; }

        public int? Phone { get; set; }  // Không bắt buộc
    }
}
