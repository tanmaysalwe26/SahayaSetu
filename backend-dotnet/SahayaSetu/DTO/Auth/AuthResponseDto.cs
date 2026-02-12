namespace SahayaSetu.DTO.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int? Id { get; set; } // Can be UserId, DonorId, or NgoId
        public string? Name { get; set; } // Optional name
    }
}
