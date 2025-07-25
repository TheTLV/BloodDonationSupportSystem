﻿namespace BloodDonationSupportSystem.DTOs.AuthDTOs
{
    public class LoginResultDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = "";
        public string Token { get; set; } = "";
    }

}
