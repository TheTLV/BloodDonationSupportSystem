using System.Numerics;
using System.Xml.Linq;
using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public User Login(string email, string password)
        {
            var user = _context.Users
                .Include(u => u.Role!)
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
                throw new Exception("Sai email hoặc mật khẩu!");

            if (user.Role == null)
                throw new Exception("Role bị null nhaaa");

            return user;
        }



        public User Register(string name, string email, string password, string phone)
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
                RoleId = 1
            };

            _context.Users.Add(user);
            _context.SaveChanges();


            return user;
        }
    }
}
