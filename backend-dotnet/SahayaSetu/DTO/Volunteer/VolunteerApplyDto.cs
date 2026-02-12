using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.Volunteer
{
    public class VolunteerApplyDto
    {
        [Required]
        public int VolunteerRequestId { get; set; }

        [Required]
        public int DonorId { get; set; }
    }
}
