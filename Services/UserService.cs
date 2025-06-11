using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.Models;

namespace BloodDonationSupportSystem.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public User Register(string name, string email, string password, string? phone)
        {
            if (_context.Users.Any(u => u.Email == email))
                throw new Exception("Email đã tồn tại trong hệ thống!");

            if (!string.IsNullOrEmpty(phone) && _context.Users.Any(u => u.PhoneNumber == phone))
            {
                throw new Exception("Số điện thoại này đã được sử dụng");
            }


            var user = new User
            {
                Fullname = name,
                Email = email,
                Password = password,
                PhoneNumber = phone,
                RId = 1
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }


        public User Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
                throw new Exception("Sai email hoặc mật khẩu!");

            return user;
        }
    }
}
