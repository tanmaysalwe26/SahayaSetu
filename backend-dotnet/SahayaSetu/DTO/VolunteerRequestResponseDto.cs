using SahayaSetu.Models;

namespace SahayaSetu.DTO
{
    public class VolunteerRequestResponseDto : RequestResponseDto
    {
        public string SkillsRequired { get; set; } = string.Empty;
        public int VolunteersRequired { get; set; }
        public long VolunteerCount { get; set; }
        public bool HasApplied { get; set; }
    }
}
