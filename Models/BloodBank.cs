using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class BloodBank
{
    public int BankId { get; set; }

    public string BloodGroup { get; set; } = null!;

    public int? QuantityMl { get; set; }

    public DateOnly? LastUpdated { get; set; }
}
