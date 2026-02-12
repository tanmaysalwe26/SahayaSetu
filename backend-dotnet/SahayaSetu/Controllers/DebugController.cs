using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SahayaSetu.Data;
using SahayaSetu.Models;
using System.Text;

namespace SahayaSetu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public DebugController(MyAppDbContext context)
        {
            _context = context;
        }

        [HttpGet("tables")]
        public async Task<IActionResult> GetTables()
        {
            try
            {
                // Raw SQL to list tables in MySQL
                var tables = new List<string>();
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SHOW TABLES";
                    await _context.Database.OpenConnectionAsync();
                    using (var result = await command.ExecuteReaderAsync())
                    {
                        while (await result.ReadAsync())
                        {
                            tables.Add(result.GetString(0));
                        }
                    }
                }
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("requests-dump")]
        public async Task<IActionResult> DumpRequests()
        {
            try 
            {
                var requests = await _context.Requests
                    .Include(r => r.ResourceRequest)
                    .Include(r => r.VolunteerRequest)
                    .Include(r => r.FundraiserRequest)
                    .Take(10)
                    .ToListAsync();
                
                var dump = requests.Select(r => new 
                {
                    r.RequestId,
                    r.NgoId,
                    Status = r.Status.ToString(),
                    StatusInt = (int)r.Status,
                    RequestType = r.RequestType.ToString(),
                    HasResource = r.ResourceRequest != null,
                    HasVolunteer = r.VolunteerRequest != null,
                    HasFundraiser = r.FundraiserRequest != null,
                    ResourceData = r.ResourceRequest, // Should serialize if present
                    r.Title
                });

                return Ok(dump);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        
        [HttpGet("ngo-check/{ngoId}")]
        public async Task<IActionResult> CheckNgo(int ngoId)
        {
             var ngo = await _context.NGOs.FindAsync(ngoId);
             if (ngo == null) return NotFound("NGO not found");
             return Ok(new { ngo.NgoId, ngo.Name, Status = ngo.Status.ToString(), StatusInt = (int)ngo.Status });
        }
    }
}
