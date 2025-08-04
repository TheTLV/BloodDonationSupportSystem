namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class DonateDetailDTO
    {
        //public int DonationId { get; set; }

        //public int? UserId { get; set; }

        //public string? BloodGroup { get; set; }

        //public string? Status { get; set; }

        //public int? Quantity { get; set; }

        //public DateOnly DonationDate { get; set; }

        //public TimeOnly DonationTime { get; set; }
        //public int? Height { get; set; }
        //public int? Weight { get; set; }
        //public string? ChronicDisease { get; set; }
        //public string? Medication { get; set; }
        public int? DonationId { get; set; }
        public string? BloodGroup { get; set; }
        public int? Quantity { get; set; }

        public DateOnly DonationDate { get; set; }

        public TimeOnly DonationTime { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }
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
    }
}
