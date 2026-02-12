using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahayaSetu.DTO;
using SahayaSetu.DTO.Volunteer; // Added back
using SahayaSetu.Services;

namespace SahayaSetu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        // View all open requests
        [HttpGet("requests")]
        public async Task<IActionResult> GetAvailableRequests()
        {
            var result = await _donorService.GetAllOpenRequestsAsync();
            return Ok(result);
        }
        
        [HttpGet("requests-for-donor")]
        public async Task<IActionResult> GetAllOpenRequestsForDonor([FromQuery] int donorId)
        {
             var result = await _donorService.GetAllOpenRequestsForDonorAsync(donorId);
             return Ok(result);
        }

        [HttpGet("fundraisers")]
        public async Task<IActionResult> GetOpenFundraisers()
        {
            var result = await _donorService.GetOpenFundraisersAsync();
            return Ok(result);
        }

        // Donate money (Fundraiser)
        [HttpPost("requests/{requestId}/donate")]
        public async Task<IActionResult> DonateMoney(int requestId, [FromQuery] int donorId, [FromBody] DonationDto dto)
        {
            await _donorService.DonateToFundraiserAsync(donorId, requestId, dto);
            return Ok("Donation processed successfully");
        }

        [HttpPost("requests/{requestId}/volunteer")]
        public async Task<IActionResult> ApplyForVolunteer(int requestId, [FromQuery] int donorId)
        {
            await _donorService.ApplyForVolunteerAsync(donorId, requestId);
            return Ok("Volunteer application submitted");
        }

        [HttpPost("requests/{requestId}/fulfill-resource")]
        public async Task<IActionResult> FulfillResource(int requestId, [FromQuery] int donorId, [FromBody] ResourceFulfillDto dto)
        {
             await _donorService.FulfillResourceRequestAsync(donorId, requestId, dto.Quantity);
             return Ok("Resource contribution recorded");
        }
        
        [HttpGet("contributions")]
        public async Task<IActionResult> GetContributions([FromQuery] int donorId)
        {
             var result = await _donorService.GetDonorContributionsAsync(donorId);
             return Ok(result);
        }

    }
}
