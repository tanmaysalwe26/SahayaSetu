using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SahayaSetu.Models
{
    public class FundraiserRequest
    {
        [Key, ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TargetAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CollectedAmount { get; set; } = 0;

        public DateTime Deadline { get; set; }

        public Request Request { get; set; } = null!;
    }
}
