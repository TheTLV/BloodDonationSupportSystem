using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Role
{
    public int RID { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
