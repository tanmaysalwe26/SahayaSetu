using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.NGO
{
    public class NgoRegisterDto
    {
        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public  string? Password { get; set; }

        [Required, MaxLength(150)]
        public string? Name { get; set; }

        [Required, MaxLength(50)]
        public string? DarpanId { get; set; }

        [Required, Phone]
        public string? ContactPhone { get; set; }

        [Required, MaxLength(200)]
        [System.Text.Json.Serialization.JsonPropertyName("addressLine1")]
        public string? Address { get; set; }

        [Required, MaxLength(50)]
        public string? City { get; set; }
    }
}
