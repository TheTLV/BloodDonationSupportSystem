using System;

public class LoginRequestDTO

{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? RememberMe { get; set; } // Nullable to allow for no value
}
