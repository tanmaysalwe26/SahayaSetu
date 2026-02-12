using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO.Auth
{
    public class AdminRegisterDto
    {
        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public string? Password { get; set; }
    }
}
