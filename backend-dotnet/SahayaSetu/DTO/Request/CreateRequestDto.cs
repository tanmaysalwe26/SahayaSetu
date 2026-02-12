using SahayaSetu.Models;
using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.Request
{
    public class CreateRequestDto
    {
        [Required]
        public int NgoId { get; set; }

        [Required]
        public RequestType RequestType { get; set; }

        [Required, MaxLength(150)]
        public string? Title { get; set; }

        [Required, MaxLength(500)]
        public string? Description { get; set; }
    }
}
