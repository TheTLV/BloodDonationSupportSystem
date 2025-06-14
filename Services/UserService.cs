﻿using BloodDonationSupportSystem.Data;
using BloodDonationSupportSystem.DTOs;
using BloodDonationSupportSystem.Models;
using BloodDonationSupportSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSupportSystem.Services
{
    public class UserService : IUserService
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

        public async Task<List<UserViewDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserViewDTO
                {
                    UserId = u.UserId,
                    Fullname = u.Fullname,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    RoleName = u.Role!.RoleName
                }).ToListAsync();

            return users;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserViewDTO> UpdateUserAsync(int id, UserUpdateDTO dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User not found");

            user.Fullname = dto.Fullname;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.RId = dto.RoleId;

            await _context.SaveChangesAsync();

            return new UserViewDTO
            {
                UserId = user.UserId,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleName = (await _context.Roles.FindAsync(dto.RoleId))?.RoleName ?? "Unknown"
            };
        }
    }
}
