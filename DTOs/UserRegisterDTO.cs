using System.ComponentModel.DataAnnotations;

namespace BloodDonationSupportSystem.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phải nhập mật khẩu")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        public required string Password { get; set; }

        [RegularExpression(@"^0\d{8,14}$", ErrorMessage = "Số điện thoại phải từ 9 đến 15 chữ số")]
        public string? PhoneNumber { get; set; }  

    }
}
