using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SahayaSetu.Models
{
    public class ResourceRequest
    {
        [Key, ForeignKey(nameof(Request))]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Resource type is required")]
        public string? ResourceType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity required must be at least 1")]
        public int QuantityRequired { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity received cannot be negative")]
        public int QuantityReceived { get; set; }

        public Request Request { get; set; }
    }
}
