using SahayaSetu.Models;

namespace SahayaSetu.DTO
{
    public class RequestResponseDto
    {
        public int RequestId { get; set; }
        public int NgoId { get; set; }
        public NgoDto? Ngo { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? RequestType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class NgoDto
    {
        public string? Name { get; set; }
    }
}
