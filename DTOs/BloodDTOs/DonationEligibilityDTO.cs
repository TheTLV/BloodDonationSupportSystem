namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class DonationEligibilityDTO
    {
        public bool? HasChronicDisease { get; set; }
        public bool? HasRecentMedication { get; set; }
        public bool? HivTestResult { get; set; }
        public bool? HepatitisB { get; set; }
        public bool? HepatitisC { get; set; }
        public bool? Syphilis { get; set; }
        public string? BloodPressure { get; set; }
        public float? HemoglobinLevel { get; set; }
        public string? Notes { get; set; }
    }
}
