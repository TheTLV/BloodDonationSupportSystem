using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class BloodRequest
{
    public int RequestId { get; set; }

    public int? UserId { get; set; }

    public string? BloodGroup { get; set; }

    public string? Status { get; set; }

    public int? Quantity { get; set; }

    public DateTime RequestDate { get; set; }

    public virtual User? User { get; set; }
}
