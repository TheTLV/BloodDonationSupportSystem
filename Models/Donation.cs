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

    public DateOnly? DonationDate { get; set; }

    public virtual User? User { get; set; }
}
