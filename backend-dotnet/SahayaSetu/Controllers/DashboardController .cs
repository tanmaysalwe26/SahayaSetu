using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SahayaSetu.Data;
using SahayaSetu.Models;

namespace SahayaSetu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public DashboardController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            var totalNgos = await _context.NGOs.CountAsync();
            var pendingNgos = await _context.NGOs.CountAsync(n => n.Status == NgoStatus.Pending);
            var totalUsers = await _context.Users.CountAsync();
            var totalRequests = await _context.Requests.CountAsync();
            var fulfilledRequests = await _context.Requests.CountAsync(r => r.Status == RequestStatus.Fulfilled);
            var openRequests = await _context.Requests.CountAsync(r => r.Status == RequestStatus.Open);

            return Ok(new
            {
                totalNgos,
                pendingNgos,
                totalUsers,
                totalRequests,
                fulfilledRequests,
                openRequests
            });
        }

        // ---------------- NGO DASHBOARD ----------------
        [HttpGet("ngo/{ngoId}")]
        public async Task<IActionResult> NgoDashboard(int ngoId)
        {
            var ngoExists = await _context.NGOs.AnyAsync(n => n.NgoId == ngoId);
            if (!ngoExists)
                return NotFound("NGO not found.");

            var totalRequests = await _context.Requests.CountAsync(r => r.NgoId == ngoId);
            var volunteerRequests = await _context.Requests.CountAsync(r =>
                r.NgoId == ngoId && r.RequestType == RequestType.Volunteer);

            var resourceRequests = await _context.Requests.CountAsync(r =>
                r.NgoId == ngoId && r.RequestType == RequestType.Resource);

            var fulfilledRequests = await _context.Requests.CountAsync(r =>
                r.NgoId == ngoId && r.Status == RequestStatus.Fulfilled);

            var openRequests = await _context.Requests.CountAsync(r =>
                r.NgoId == ngoId && r.Status == RequestStatus.Open);

            return Ok(new
            {
                totalRequests,
                volunteerRequests,
                resourceRequests,
                fulfilledRequests,
                openRequests
            });
        }

        // ---------------- DONOR DASHBOARD ----------------
        [HttpGet("donor/{donorId}")]
        public async Task<IActionResult> DonorDashboard(int donorId)
        {
            var donorExists = await _context.Donors.AnyAsync(d => d.DonorId == donorId);
            if (!donorExists)
                return NotFound("Donor not found.");

            var totalApplications = await _context.VolunteerParticipations
                .CountAsync(vp => vp.DonorId == donorId);

            var completedApplications = await _context.VolunteerParticipations
                .CountAsync(vp => vp.DonorId == donorId && vp.Status == VolunteerStatus.Completed);

            return Ok(new
            {
                totalApplications,
                completedApplications
            });
        }
    }
}
