using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.Request
{
    public class CreateFundraiserRequestDto
    {
        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Target amount must be positive")]
        public decimal TargetAmount { get; set; }

        public DateTime? Deadline { get; set; }
    }
}
