using System;
using System.Collections.Generic;

namespace BloodDonationSupportSystem.Models;

public partial class DonationEligibility
{
    public int EligibilityId { get; set; }

    public int UserId { get; set; }

    public int? DonationId { get; set; }

    public DateOnly ScreeningDate { get; set; }

    public bool? HasChronicDisease { get; set; }

    public bool? HasRecentMedication { get; set; }

    public bool? HivTestResult { get; set; }

    public bool? HepatitisB { get; set; }

    public bool? HepatitisC { get; set; }

    public bool? Syphilis { get; set; }

    public string? BloodPressure { get; set; }

    public float? HemoglobinLevel { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }
    public float? TemperatureC { get; set; }
    public int? HeartRateBpm { get; set; }
    public string? CurrentHealthStatus { get; set; }
    public string? MedicalHistory { get; set; }

    public virtual Donation? Donation { get; set; }

    public virtual User User { get; set; } = null!;
}
