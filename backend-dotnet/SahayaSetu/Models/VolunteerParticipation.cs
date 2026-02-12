using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SahayaSetu.Models
{
    public enum VolunteerStatus
    {
        Applied,
        Completed
    }
    public class VolunteerParticipation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Volunteer request is required")]
        public int VolunteerRequestId { get; set; }

        [ForeignKey(nameof(VolunteerRequestId))]
        public VolunteerRequest? VolunteerRequest { get; set; }

        [Required(ErrorMessage = "Donor is required")]
        public int DonorId { get; set; }

        [ForeignKey(nameof(DonorId))]
        public Donor? Donor { get; set; }

        public VolunteerStatus Status { get; set; } = VolunteerStatus.Applied;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
