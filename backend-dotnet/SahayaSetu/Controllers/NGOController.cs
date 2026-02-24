using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahayaSetu.DTO.NGO;
using SahayaSetu.DTO.Request;
using SahayaSetu.DTO; // Added back
using SahayaSetu.Services;

namespace SahayaSetu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NGOController : ControllerBase
    {
        private readonly INgoService _ngoService;

        public NGOController(INgoService ngoService)
        {
            _ngoService = ngoService;
        }

        // ---------------- CREATE RESOURCE REQUEST ----------------
        [HttpPost("requests/resource")]
        public async Task<IActionResult> CreateResourceRequest([FromBody] CreateResourceRequestDto dto, [FromQuery] int ngoId)
        {
            await _ngoService.CreateResourceRequestAsync(ngoId, dto);
            return Ok("Resource Request created successfully");
        }

        // ---------------- CREATE VOLUNTEER REQUEST ----------------
        [HttpPost("requests/volunteer")]
        public async Task<IActionResult> CreateVolunteerRequest([FromBody] CreateVolunteerRequestDto dto, [FromQuery] int ngoId)
        {
            await _ngoService.CreateVolunteerRequestAsync(ngoId, dto);
            return Ok("Volunteer Request created successfully");
        }
        
        // ---------------- CREATE FUNDRAISER REQUEST ----------------
        [HttpPost("requests/fundraiser")]
        public async Task<IActionResult> CreateFundraiserRequest([FromBody] CreateFundraiserRequestDto dto, [FromQuery] int ngoId)
        {
            await _ngoService.CreateFundraiserRequestAsync(ngoId, dto);
            return Ok("Fundraiser Request created successfully");
        }

        // ---------------- VIEW OWN REQUESTS ----------------
        [HttpGet("{ngoId}/requests")]
        public async Task<IActionResult> ViewOwnRequests(int ngoId)
        {
            var result = await _ngoService.GetNgoRequestsAsync(ngoId);
            return Ok(result);
        }

        // ---------------- VIEW REQUEST DETAILS ----------------
        [HttpGet("requests/{requestId}/details")]
        public async Task<IActionResult> ViewRequestDetails(int requestId)
        {
            var result = await _ngoService.GetRequestDetailsAsync(requestId);
            return Ok(result);
        }
        
        // ---------------- UPDATE NGO PROFILE ----------------
        [HttpPut("{ngoId}")]
        public async Task<IActionResult> UpdateNgoDetails(int ngoId, [FromBody] UpdateNgoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _ngoService.UpdateNgoProfileAsync(ngoId, dto);
            return Ok("NGO details updated successfully.");
        }

        // ---------------- FULFILL REQUEST ----------------
        [HttpPut("requests/{requestId}/fulfill")]
        public async Task<IActionResult> FulfillRequest(int requestId)
        {
            await _ngoService.FulfillRequestAsync(requestId);
            return Ok("Request marked as fulfilled");
        }
        
        // ---------------- CLOSE REQUEST (Explicit) ----------------
        [HttpPut("requests/{requestId}/close")]
        public async Task<IActionResult> CloseRequest(int requestId, [FromQuery] int ngoId)
        {
            await _ngoService.CloseRequestAsync(requestId, ngoId);
            return Ok("Request closed successfully");
        }
        
        // ---------------- GET DONATIONS ----------------
        [HttpGet("{ngoId}/donations")]
        public async Task<IActionResult> GetDonations(int ngoId)
        {
            var result = await _ngoService.GetNgoDonationsAsync(ngoId);
            return Ok(result);
        }
        
        // ---------------- GET FUNDRAISER DONATIONS ----------------
        [HttpGet("fundraiser-donations")]
        public async Task<IActionResult> GetFundraiserDonations([FromQuery] int ngoId)
        {
            var result = await _ngoService.GetFundraiserDonationsAsync(ngoId);
            return Ok(result);
        }
        
        // ---------------- SEND DONATION RESPONSE ----------------
        [HttpPost("donation-response")]
        public async Task<IActionResult> SendDonationResponse([FromBody] DonationResponseRequestDto dto)
        {
            await _ngoService.SendDonationResponseAsync(dto);
            return Ok("Response sent successfully");
        }
    }
}
