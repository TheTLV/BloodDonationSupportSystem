namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class PreDonationScreeningDTO
    {
        public float? TemperatureC { get; set; }
        public int? HeartRateBpm { get; set; }
        public string? CurrentHealthStatus { get; set; }
        public string? BloodPressure { get; set; }
        public string? MedicalHistory { get; set; }
    }
}
