using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.NGO
{
    public class DonationResponseRequestDto
    {
        [Required]
        public int DonationId { get; set; }
        
        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
