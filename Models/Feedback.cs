using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? CreatedBy { get; set; }

    public string? FeedbackType { get; set; }

    public string? Content { get; set; }

    public DateOnly? ReportDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }
}
