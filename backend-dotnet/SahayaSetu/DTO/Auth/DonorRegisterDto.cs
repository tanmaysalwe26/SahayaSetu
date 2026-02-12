using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.Auth
{
    public class DonorRegisterDto
    {
        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public string? Password { get; set; }

        [Required, MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, Phone]
        public string? Phone { get; set; }

        [Required, MaxLength(200)]
        public string? Address { get; set; }

        [Required, MaxLength(50)]
        public string? City { get; set; }
    }
}
