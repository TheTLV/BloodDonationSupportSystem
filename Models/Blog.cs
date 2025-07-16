using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? Title { get; set; }

    public string? Image { get; set; }

    public string? Link { get; set; }

    public string? Description { get; set; }
}
