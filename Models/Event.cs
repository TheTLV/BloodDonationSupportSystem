using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Event
{
    public int EventId { get; set; }

    public int CreatedBy { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public DateOnly EventDate { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
