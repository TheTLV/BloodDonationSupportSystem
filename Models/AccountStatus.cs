using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class AccountStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
