using System.Net;
using System.Numerics;
using System.Xml.Linq;
using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.DTOs.AuthDTOs;
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

        //public User Login(string email, string password)
        //{
        //    var user = _context.Users
        //        .Include(u => u.Role!)
        //        .FirstOrDefault(u => u.Email == email && u.Password == password);

        //    if (user == null)
        //        throw new Exception("Sai email hoặc mật khẩu!");

        //    if (user.Role == null)
        //        throw new Exception("Role bị null nhaaa");

        //    return user;
        //}
        public User Login(UserLoginDTO dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
                throw new Exception("Sai tài khoản hoặc mật khẩu");

            if (user.Role == null)
                throw new Exception("User không có Role, dữ liệu sai");

            return user; 
        }



        //public User Register(string name, string email, string password, string? phone, string gender, DateOnly dob, string address)
        //{
        //    var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
        //    if (existingUser != null)
        //        throw new Exception("Email đã được sử dụng");

        //    var user = new User
        //    {
        //        Fullname = name,
        //        Email = email,
        //        Password = password, 
        //        PhoneNumber = phone,
        //        RoleId = 1 
        //    };

        //    var profile = new Profile
        //    {
        //        User = user,
        //        Gender = gender,
        //        Address = address,
        //        DateOfBirth = dob
        //    };

        //    _context.Users.Add(user);
        //    _context.Profiles.Add(profile);
        //    _context.SaveChanges();

        //    return user;
        //}

        public User Register(UserRegisterDTO dto)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            var exitstingPhone = _context.Users.FirstOrDefault(u => u.PhoneNumber == dto.PhoneNumber);
            if (existingUser != null || exitstingPhone != null)
                throw new Exception("Email hoặc số điện thoại đã được sử dụng");

            var user = new User
            {
                Fullname = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                RoleId = 1
            };

            var profile = new Profile
            {
                User = user,
                Gender = dto.Gender,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth
            };

            _context.Users.Add(user);
            _context.Profiles.Add(profile);
            _context.SaveChanges();

            return user;
        }


    }
}
