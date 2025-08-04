namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class BloodBankViewDTO
    {
        public string BloodGroup { get; set; } = default!;
        public int QuantityMl { get; set; }
        public DateOnly? LastUpdated { get; set; }
    }

}
