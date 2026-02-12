namespace SahayaSetu.DTO
{
    public class DonorResponseDto
    {
        public int DonorId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
