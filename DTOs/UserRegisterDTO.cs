﻿namespace BloodDonationSupportSystem.DTOs
{
    public class UserRegisterDTO
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public int? Phone { get; set; }
    }
}
