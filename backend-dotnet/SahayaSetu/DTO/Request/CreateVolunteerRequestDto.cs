using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.Request
{
    public class CreateVolunteerRequestDto
    {
        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(500)]
        public string Description { get; set; } = null!;

        [MaxLength(200)]
        public string? SkillsRequired { get; set; }

        [Range(1, int.MaxValue)]
        public int VolunteersRequired { get; set; }
    }
}
