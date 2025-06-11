using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public int? CreatedBy { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
}
