using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Profile
{
    public int ProfileId { get; set; }

    public int? UserId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? BloodGroup { get; set; }

    public virtual User? User { get; set; }
}
