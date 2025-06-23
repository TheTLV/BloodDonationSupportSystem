using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int? RoleId { get; set; }

    public string Fullname { get; set; } = null!;

    public virtual ICollection<Bloodrequest> Bloodrequests { get; } = new List<Bloodrequest>();

    public virtual ICollection<Donation> Donations { get; } = new List<Donation>();

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual Profile? Profile { get; set; }

    public virtual Role? Role { get; set; }
}
