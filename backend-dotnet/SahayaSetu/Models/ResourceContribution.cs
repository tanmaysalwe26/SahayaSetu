using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SahayaSetu.Models
{
    public class ResourceContribution
    {
        [Key]
        public int ContributionId { get; set; }

        [Required]
        public int DonorId { get; set; }

        [ForeignKey(nameof(DonorId))]
        public Donor Donor { get; set; } = null!;

        [Required]
        public int ResourceRequestId { get; set; }

        [ForeignKey(nameof(ResourceRequestId))]
        public ResourceRequest ResourceRequest { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        public DateTime ContributedAt { get; set; } = DateTime.UtcNow;
    }
}
