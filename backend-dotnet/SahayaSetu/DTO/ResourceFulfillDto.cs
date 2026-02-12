using System.ComponentModel.DataAnnotations;

namespace SahayaSetu.DTO
{
    public class ResourceFulfillDto
    {
        public int RequestId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
