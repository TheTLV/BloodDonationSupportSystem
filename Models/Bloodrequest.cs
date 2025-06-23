using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Bloodrequest
{
    public int RequestId { get; set; }

    public int? UserId { get; set; }

    public string? BloodGroup { get; set; }

    public string? Status { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? RequestDate { get; set; }

    public TimeOnly? RequestTime { get; set; }

    public virtual User? User { get; set; }
}
