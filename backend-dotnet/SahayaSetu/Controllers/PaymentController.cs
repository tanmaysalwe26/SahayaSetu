using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SahayaSetu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] JsonElement data)
        {
            try
            {
                var amount = data.GetProperty("amount").GetInt32();
                
                // Create a simple order response (matching Java structure)
                var order = new
                {
                    id = $"order_{Guid.NewGuid()}",
                    amount = amount * 100, // Convert to paise
                    currency = "INR",
                    receipt = "donation_receipt_001",
                    status = "created"
                };

                return Ok(JsonSerializer.Serialize(order));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating order: {ex.Message}");
            }
        }

        [HttpPost("success")]
        public async Task<IActionResult> UpdateDonation([FromBody] JsonElement data)
        {
            try
            {
                var requestId = data.GetProperty("requestId").GetInt32();
                var donorId = data.GetProperty("donorId").GetInt32();
                var amount = data.GetProperty("amount").GetDecimal();
                var paymentId = data.GetProperty("paymentId").GetString();

                // This would integrate with your donation service
                // For now, return success to match Java behavior
                return Ok("Donation recorded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error recording donation: {ex.Message}");
            }
        }
    }
}