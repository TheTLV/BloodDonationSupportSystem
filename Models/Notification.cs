﻿using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public int? EventId { get; set; }

    public string? Message { get; set; }

    public DateOnly NotifDate { get; set; }

    public bool IsRead { get; set; }= false;

    public virtual Event? Event { get; set; }

    public virtual User? User { get; set; }
}
