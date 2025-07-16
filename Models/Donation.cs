using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Donation
{
    public int DonationId { get; set; }

    public int? UserId { get; set; }

    public string? BloodGroup { get; set; }

    public string? Status { get; set; }

    public int? Quantity { get; set; }

    public DateOnly DonationDate { get; set; }

    public TimeOnly DonationTime { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public string? ChronicDisease { get; set; }
    public string? Medication { get; set; }
    public DateOnly? LastDonationDate { get; set; }


    public virtual User? User { get; set; }
}
