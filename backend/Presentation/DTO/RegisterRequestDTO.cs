using System;

public class RegisterRequestDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; } // Nullable to allow for no value
    public DateTime DateOfBirth { get; set; }
}

