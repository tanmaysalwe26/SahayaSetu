using SahayaSetu.Models;

namespace SahayaSetu.DTO
{
    public class ResourceRequestResponseDto : RequestResponseDto
    {
        public string? ResourceType { get; set; }
        public int QuantityRequired { get; set; }
        public int QuantityReceived { get; set; }
    }
}
