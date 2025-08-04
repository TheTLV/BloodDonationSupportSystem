using System.ComponentModel.DataAnnotations;

namespace BloodDonationSupportSystem.DTOs.BloodDTOs
{
    public class UpdateBloodTypeDTO
    {
        [Required]
        public string BloodType { get; set; } = default!;
    }
}
