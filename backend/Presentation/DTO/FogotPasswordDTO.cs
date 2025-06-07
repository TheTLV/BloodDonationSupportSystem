using System;

public class FogotPasswordDTO
{
    public string Email { get; set; }
    public DateTime RequestDate { get; set; }
    public string? SecurityQuestionAnswer { get; set; } // Nullable to allow for no value
}
