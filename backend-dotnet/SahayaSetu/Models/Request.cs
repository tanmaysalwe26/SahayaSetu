using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SahayaSetu.Models
{
    public enum RequestType
    {
        Resource=1,
        Volunteer=2,
        Fundraiser=3
    }
    public enum RequestStatus
    {
        Open,
        Fulfilled,
        Closed
    }

    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "NGO is required")]
        public int NgoId { get; set; }

        [ForeignKey(nameof(NgoId))]
        public NGO NGO { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(20)")]
        public RequestType RequestType { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public RequestStatus Status { get; set; } = RequestStatus.Open;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiresAt { get; set; }

        // Navigation
        public ResourceRequest? ResourceRequest { get; set; }
        public VolunteerRequest? VolunteerRequest { get; set; }
        public FundraiserRequest? FundraiserRequest { get; set; }
    }
}
