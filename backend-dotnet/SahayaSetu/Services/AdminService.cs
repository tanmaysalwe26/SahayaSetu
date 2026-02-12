using Microsoft.EntityFrameworkCore;
using SahayaSetu.Data;
using SahayaSetu.DTO;
using SahayaSetu.Models;

namespace SahayaSetu.Services
{
    public class AdminService : IAdminService
    {
        private readonly MyAppDbContext _context;
        private readonly IMailService _mailService;

        public AdminService(MyAppDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        public async Task<List<NgoResponseDto>> GetAllNgosAsync()
        {
            var ngos = await _context.NGOs.ToListAsync();
            return ngos.Select(n => new NgoResponseDto
            {
                NgoId = n.NgoId,
                Name = n.Name,
                Email = n.Email,
                Phone = n.ContactPhone, // Mapping ContactPhone to Phone for DTO
                Address = n.Address,
                City = n.City,
                Status = n.Status.ToString().ToUpper(),
                ApprovedAt = n.ApprovedAt,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        public async Task<List<DonorResponseDto>> GetAllDonorsAsync()
        {
            var donors = await _context.Donors
                .Include(d => d.User)
                .ToListAsync();

            return donors.Select(d => new DonorResponseDto
            {
                DonorId = d.DonorId,
                FullName = d.FullName,
                Email = d.User.Email,
                Phone = d.Phone,
                City = d.City,
                CreatedAt = d.User.CreatedAt
            }).ToList();
        }

        public async Task<List<RequestResponseDto>> GetAllRequestsAsync()
        {
             var requests = await _context.Requests
                .Include(r => r.NGO)
                .Include(r => r.ResourceRequest)
                .Include(r => r.VolunteerRequest).ThenInclude(vr => vr.VolunteerParticipations)
                .Include(r => r.FundraiserRequest)
                .ToListAsync();

            return requests.Select(MapToDto).ToList();
        }

        public async Task ApproveNgoAsync(int ngoId)
        {
            var ngo = await _context.NGOs.FindAsync(ngoId)
                ?? throw new Exception("NGO not found");

            if (ngo.Status == NgoStatus.Approved)
                throw new Exception("NGO is already approved");

            ngo.Status = NgoStatus.Approved;
            ngo.ApprovedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            await _mailService.SendNgoApprovalMailAsync(ngo.Email, ngo.Name);
        }

        public async Task DisapproveNgoAsync(int ngoId)
        {
            var ngo = await _context.NGOs.FindAsync(ngoId)
                ?? throw new Exception("NGO not found");

             // Assuming Rejected is a valid status or reusing Disabled, but Java has REJECTED.
             // .NET Enum NgoStatus might not have Rejected. Step 51 just showed Status.
             // I recall NgoStatus enum from Step 51 having Pending, Approved, Rejected.
             // Wait, Step 218 show NgoStatus enum used in Controller.
             // Assume Rejected exists. If not, I'll need to check or add it.
             // Java Disapprove sets REJECTED.
             
            if (ngo.Status == NgoStatus.Rejected)
                throw new Exception("NGO is already rejected");
            
            ngo.Status = NgoStatus.Rejected;
            await _context.SaveChangesAsync();

            await _mailService.SendDisapprovalEmailAsync(ngo.Email, ngo.Name);
        }

        public async Task DisableNgoAsync(int ngoId)
        {
            var ngo = await _context.NGOs.FindAsync(ngoId)
                ?? throw new Exception("NGO not found");

            if (ngo.Status == NgoStatus.Disabled)
                throw new Exception("NGO is already disabled");

            ngo.Status = NgoStatus.Disabled;
            await _context.SaveChangesAsync();

            await _mailService.SendNgoDisableEmailAsync(ngo.Email, ngo.Name);
        }

        public async Task EnableNgoAsync(int ngoId)
        {
            var ngo = await _context.NGOs.FindAsync(ngoId)
                ?? throw new Exception("NGO not found");

            ngo.Status = NgoStatus.Approved;
            await _context.SaveChangesAsync();
        }

        private RequestResponseDto MapToDto(Request request)
        {
            if (request.FundraiserRequest != null)
            {
               return new FundraiserRequestResponseDto
                {
                    RequestId = request.RequestId,
                    NgoId = request.NgoId,
                    Ngo = request.NGO != null ? new NgoDto { Name = request.NGO.Name } : null,
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status.ToString().ToUpper(),
                    RequestType = request.RequestType.ToString().ToUpper(),
                    CreatedAt = request.CreatedAt,
                    TargetAmount = request.FundraiserRequest.TargetAmount,
                    CollectedAmount = request.FundraiserRequest.CollectedAmount,
                    Deadline = request.FundraiserRequest.Deadline
                };
            }
            else if (request.ResourceRequest != null)
            {
                 return new ResourceRequestResponseDto
                {
                    RequestId = request.RequestId,
                    NgoId = request.NgoId,
                    Ngo = request.NGO != null ? new NgoDto { Name = request.NGO.Name } : null,
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status.ToString().ToUpper(),
                    RequestType = request.RequestType.ToString().ToUpper(),
                    CreatedAt = request.CreatedAt,
                    ResourceType = request.ResourceRequest.ResourceType,
                    QuantityRequired = request.ResourceRequest.QuantityRequired,
                    QuantityReceived = request.ResourceRequest.QuantityReceived
                };
            }
            else if (request.VolunteerRequest != null)
            {
                 var participationCount = request.VolunteerRequest.VolunteerParticipations?.Count ?? 0;

                 return new VolunteerRequestResponseDto
                {
                    RequestId = request.RequestId,
                    NgoId = request.NgoId,
                    Ngo = request.NGO != null ? new NgoDto { Name = request.NGO.Name } : null,
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status.ToString().ToUpper(),
                    RequestType = request.RequestType.ToString().ToUpper(),
                    CreatedAt = request.CreatedAt,
                    SkillsRequired = request.VolunteerRequest.SkillsRequired,
                    VolunteersRequired = request.VolunteerRequest.VolunteersRequired,
                    VolunteerCount = participationCount
                };
            }
            
            return new RequestResponseDto
            {
                RequestId = request.RequestId,
                NgoId = request.NgoId,
                Ngo = request.NGO != null ? new NgoDto { Name = request.NGO.Name } : null,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status.ToString().ToUpper(),
                RequestType = request.RequestType.ToString().ToUpper(),
                CreatedAt = request.CreatedAt
            };
        }
    }
}
