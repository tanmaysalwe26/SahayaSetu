using Azure.Core;
using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.Models
{
    public enum NgoStatus
    {
        Pending=0,
        Approved=1,
        Rejected=2,
        Disabled=3
    }
    public class NGO
    {
        [Key]
        public int NgoId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? PasswordHash { get; set; }

        [Required(ErrorMessage = "NGO name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Darpan ID is required")]
        public string? DarpanId { get; set; }

        [Required(ErrorMessage = "Contact phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? ContactPhone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required]
        public NgoStatus Status { get; set; } = NgoStatus.Pending;

        public DateTime? ApprovedAt { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
