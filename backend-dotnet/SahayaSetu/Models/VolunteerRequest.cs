using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SahayaSetu.Models
{
    public class VolunteerRequest
    {
        [Key, ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Skills are required")]
        public string SkillsRequired { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Volunteers required must be at least 1")]
        public int VolunteersRequired { get; set; }

        public Request Request { get; set; } = null!;

        public ICollection<VolunteerParticipation> VolunteerParticipations { get; set; }
            = new List<VolunteerParticipation>();
    }
}
