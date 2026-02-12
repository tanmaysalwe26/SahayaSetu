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
            try {
                await _ngoService.CreateResourceRequestAsync(ngoId, dto);
                return Ok("Resource Request created successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // ---------------- CREATE VOLUNTEER REQUEST ----------------
        [HttpPost("requests/volunteer")]
        public async Task<IActionResult> CreateVolunteerRequest([FromBody] CreateVolunteerRequestDto dto, [FromQuery] int ngoId)
        {
            try {
                await _ngoService.CreateVolunteerRequestAsync(ngoId, dto);
                return Ok("Volunteer Request created successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        // ---------------- CREATE FUNDRAISER REQUEST ----------------
        [HttpPost("requests/fundraiser")]
        public async Task<IActionResult> CreateFundraiserRequest([FromBody] CreateFundraiserRequestDto dto, [FromQuery] int ngoId)
        {
            try {
                await _ngoService.CreateFundraiserRequestAsync(ngoId, dto);
                return Ok("Fundraiser Request created successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // ---------------- VIEW OWN REQUESTS ----------------
        [HttpGet("{ngoId}/requests")]
        public async Task<IActionResult> ViewOwnRequests(int ngoId)
        {
            try {
                var result = await _ngoService.GetNgoRequestsAsync(ngoId);
                return Ok(result);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // ---------------- VIEW REQUEST DETAILS ----------------
        [HttpGet("requests/{requestId}/details")]
        public async Task<IActionResult> ViewRequestDetails(int requestId)
        {
             try {
                var result = await _ngoService.GetRequestDetailsAsync(requestId);
                return Ok(result);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        // ---------------- UPDATE NGO PROFILE ----------------
        [HttpPut("{ngoId}")]
        public async Task<IActionResult> UpdateNgoDetails(int ngoId, [FromBody] UpdateNgoDto dto)
        {
             if (!ModelState.IsValid) return BadRequest(ModelState);
             try {
                await _ngoService.UpdateNgoProfileAsync(ngoId, dto);
                return Ok("NGO details updated successfully.");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // ---------------- FULFILL REQUEST ----------------
        [HttpPut("requests/{requestId}/fulfill")]
        public async Task<IActionResult> FulfillRequest(int requestId)
        {
             try {
                await _ngoService.FulfillRequestAsync(requestId);
                return Ok("Request marked as fulfilled");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        // ---------------- CLOSE REQUEST (Explicit) ----------------
        [HttpPut("requests/{requestId}/close")]
        public async Task<IActionResult> CloseRequest(int requestId, [FromQuery] int ngoId)
        {
             try {
                await _ngoService.CloseRequestAsync(requestId, ngoId);
                return Ok("Request closed successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        // ---------------- GET DONATIONS ----------------
        [HttpGet("{ngoId}/donations")]
        public async Task<IActionResult> GetDonations(int ngoId)
        {
             try {
                var result = await _ngoService.GetNgoDonationsAsync(ngoId);
                return Ok(result);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        // ---------------- GET FUNDRAISER DONATIONS ----------------
        [HttpGet("fundraiser-donations")]
        public async Task<IActionResult> GetFundraiserDonations([FromQuery] int ngoId)
        {
             try {
                var result = await _ngoService.GetFundraiserDonationsAsync(ngoId);
                return Ok(result);
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        // ---------------- SEND DONATION RESPONSE ----------------
        [HttpPost("donation-response")]
        public async Task<IActionResult> SendDonationResponse([FromBody] DonationResponseRequestDto dto)
        {
             try {
                await _ngoService.SendDonationResponseAsync(dto);
                return Ok("Response sent successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
