using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SahayaSetu.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        public int DonorId { get; set; }

        [ForeignKey(nameof(DonorId))]
        public Donor Donor { get; set; } = null!;

        [Required]
        public int FundraiserRequestId { get; set; }

        [ForeignKey(nameof(FundraiserRequestId))]
        public FundraiserRequest FundraiserRequest { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMode PaymentMode { get; set; }

        public string? PaymentReference { get; set; }

        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;

        public bool ResponseStatus { get; set; } = false;

        public string? ResponseMessage { get; set; }

        public DateTime? ResponseSentAt { get; set; }
    }
}
