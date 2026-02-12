using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO
{
    public class UpdateNgoDto
    {
        [Required(ErrorMessage = "NGO name is required")]
        [MaxLength(150, ErrorMessage = "NGO name cannot exceed 150 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Contact phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? ContactPhone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string? City { get; set; } 
    }
}
