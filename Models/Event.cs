using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int? CreatedBy { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? EventDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
