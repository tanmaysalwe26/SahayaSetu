using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.Request
{
    public class CreateResourceRequestDto
    {
        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(500)]
        public string Description { get; set; } = null!;

        [Required, MaxLength(100)]
        public string ResourceType { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int QuantityRequired { get; set; }
    }
}
