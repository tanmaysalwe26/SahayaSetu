using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahayaSetu.Services;

namespace SahayaSetu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("ngos")]
        public async Task<IActionResult> GetAllNgos()
        {
            var result = await _adminService.GetAllNgosAsync();
            return Ok(result);
        }

        [HttpGet("donors")]
        public async Task<IActionResult> GetAllDonors()
        {
            var result = await _adminService.GetAllDonorsAsync();
            return Ok(result);
        }
        
        [HttpGet("requests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var result = await _adminService.GetAllRequestsAsync();
            return Ok(result);
        }

        [HttpPut("ngos/{ngoId}/approve")]
        public async Task<IActionResult> ApproveNgo(int ngoId)
        {
            try {
                await _adminService.ApproveNgoAsync(ngoId);
                return Ok("NGO approved successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("ngos/{ngoId}/disable")]
        public async Task<IActionResult> DisableNgo(int ngoId)
        {
            try {
               await _adminService.DisableNgoAsync(ngoId);
               return Ok("NGO disabled successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        [HttpPut("ngos/{ngoId}/disapprove")]
        public async Task<IActionResult> DisapproveNgo(int ngoId)
        {
            try {
               await _adminService.DisapproveNgoAsync(ngoId);
               return Ok("NGO disapproved successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        [HttpPut("ngos/{ngoId}/enable")]
        public async Task<IActionResult> EnableNgo(int ngoId)
        {
            try {
               await _adminService.EnableNgoAsync(ngoId);
               return Ok("NGO enabled successfully");
            } catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}
