using SahayaSetu.Models;
using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO
{
    public class DonationDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMode PaymentMode { get; set; }

        public string? PaymentReference { get; set; }
    }
}
