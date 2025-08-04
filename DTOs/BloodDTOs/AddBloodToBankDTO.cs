namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class AddBloodToBankDTO
    {
        public string BloodGroup { get; set; } = default!;
        public int QuantityMl { get; set; }
    }
}
