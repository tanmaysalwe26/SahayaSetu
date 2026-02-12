using Microsoft.EntityFrameworkCore;
using SahayaSetu.Data;
using SahayaSetu.DTO;
using SahayaSetu.Models;

namespace SahayaSetu.Services
{
    public class DonorService : IDonorService
    {
        private readonly MyAppDbContext _context;

        public DonorService(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestResponseDto>> GetAllOpenRequestsAsync()
        {
            var requests = await _context.Requests
                .Include(r => r.NGO)
                .Include(r => r.ResourceRequest)
                .Include(r => r.VolunteerRequest)
                .Include(r => r.FundraiserRequest)
                .Where(r => r.Status == RequestStatus.Open)
                .ToListAsync();

            return requests.Select(r => MapToDto(r)).ToList();
        }

        public async Task<List<RequestResponseDto>> GetAllOpenRequestsForDonorAsync(int donorId)
        {
            var requests = await _context.Requests
                .Include(r => r.NGO)
                .Include(r => r.ResourceRequest)
                .Include(r => r.VolunteerRequest)
                .ThenInclude(vr => vr.VolunteerParticipations)
                .Include(r => r.FundraiserRequest)
                .Where(r => r.Status == RequestStatus.Open)
                .ToListAsync();

            return requests.Select(r => MapToDto(r, donorId)).ToList();
        }

        public async Task<List<FundraiserRequestResponseDto>> GetOpenFundraisersAsync()
        {
            var requests = await _context.FundraiserRequests
                .Include(fr => fr.Request)
                .ThenInclude(r => r.NGO)
                .Where(fr => fr.Request.Status == RequestStatus.Open)
                .ToListAsync();

            return requests.Select(fr => {
                var dto = new FundraiserRequestResponseDto
                {
                    RequestId = fr.Request.RequestId,
                    NgoId = fr.Request.NgoId,
                    Ngo = fr.Request.NGO != null ? new NgoDto { Name = fr.Request.NGO.Name } : null,
                    Title = fr.Request.Title,
                    Description = fr.Request.Description,
                    Status = fr.Request.Status.ToString().ToUpper(),
                    RequestType = fr.Request.RequestType.ToString().ToUpper(),
                    CreatedAt = fr.Request.CreatedAt,
                    TargetAmount = fr.TargetAmount,
                    CollectedAmount = fr.CollectedAmount,
                    Deadline = fr.Deadline
                };
                return dto;
            }).ToList();
        }

        public async Task DonateToFundraiserAsync(int donorId, int requestId, DonationDto dto)
        {
            var donor = await _context.Donors.FindAsync(donorId)
                ?? throw new Exception("Donor not found");

            var fundraiser = await _context.FundraiserRequests
                .Include(fr => fr.Request)
                .FirstOrDefaultAsync(fr => fr.RequestId == requestId)
                ?? throw new Exception("Fundraiser not found");

            if (fundraiser.Request.Status != RequestStatus.Open)
                throw new Exception("Fundraiser is closed");

            var donation = new Donation
            {
                DonorId = donorId,
                FundraiserRequestId = requestId,
                Amount = dto.Amount,
                PaymentMode = dto.PaymentMode,
                PaymentReference = dto.PaymentReference,
                DonatedAt = DateTime.UtcNow
            };

            _context.Donations.Add(donation);

            // Update collected amount
            fundraiser.CollectedAmount += dto.Amount;

            // Auto-close if target reached
            if (fundraiser.CollectedAmount >= fundraiser.TargetAmount)
            {
                fundraiser.Request.Status = RequestStatus.Fulfilled;
            }

            await _context.SaveChangesAsync();
        }

        public async Task ApplyForVolunteerAsync(int donorId, int requestId)
        {
            var donor = await _context.Donors.FindAsync(donorId)
                ?? throw new Exception("Donor not found");

            var volunteerRequest = await _context.VolunteerRequests
                .Include(vr => vr.Request)
                .Include(vr => vr.VolunteerParticipations)
                .FirstOrDefaultAsync(vr => vr.RequestId == requestId)
                ?? throw new Exception("Volunteer request not found");

            if (volunteerRequest.Request.Status != RequestStatus.Open)
                throw new Exception("Volunteer opportunity is closed");

            if (volunteerRequest.VolunteerParticipations.Any(vp => vp.DonorId == donorId))
                throw new Exception("You have already applied for this volunteer opportunity");

            var participation = new VolunteerParticipation
            {
                VolunteerRequestId = requestId,
                DonorId = donorId,
                Status = VolunteerStatus.Applied,
                JoinedAt = DateTime.UtcNow
            };

            _context.VolunteerParticipations.Add(participation);
            await _context.SaveChangesAsync();
        }

        public async Task FulfillResourceRequestAsync(int donorId, int requestId, int quantity)
        {
            var donor = await _context.Donors.FindAsync(donorId)
                ?? throw new Exception("Donor not found");

            var resourceRequest = await _context.ResourceRequests
                .Include(rr => rr.Request)
                .FirstOrDefaultAsync(rr => rr.RequestId == requestId)
                ?? throw new Exception("Resource request not found");

            if (resourceRequest.Request.Status != RequestStatus.Open)
                throw new Exception("Resource request is closed");

            if (resourceRequest.QuantityReceived + quantity > resourceRequest.QuantityRequired)
                throw new Exception($"Quantity exceeds requirement. Needed: {resourceRequest.QuantityRequired - resourceRequest.QuantityReceived}");

            resourceRequest.QuantityReceived += quantity;
            if (resourceRequest.QuantityReceived >= resourceRequest.QuantityRequired)
            {
                resourceRequest.Request.Status = RequestStatus.Fulfilled;
            }

            var contribution = new ResourceContribution
            {
                DonorId = donorId,
                ResourceRequestId = requestId,
                Quantity = quantity,
                ContributedAt = DateTime.UtcNow
            };

            _context.ResourceContributions.Add(contribution);
            await _context.SaveChangesAsync();
        }

        public async Task<ContributionSummaryDto> GetDonorContributionsAsync(int donorId)
        {
            var donations = await _context.Donations
                .Include(d => d.FundraiserRequest).ThenInclude(fr => fr.Request)
                .Where(d => d.DonorId == donorId)
                .ToListAsync();

            var volunteerings = await _context.VolunteerParticipations
                .Include(vp => vp.VolunteerRequest).ThenInclude(vr => vr.Request)
                .Where(vp => vp.DonorId == donorId)
                .ToListAsync();

            var resources = await _context.ResourceContributions
                .Include(rc => rc.ResourceRequest).ThenInclude(rr => rr.Request)
                .Where(rc => rc.DonorId == donorId)
                .ToListAsync();

            return new ContributionSummaryDto
            {
                Donations = donations.Select(d => new DonationContributionDto
                {
                    Amount = d.Amount,
                    FundraiserTitle = d.FundraiserRequest.Request.Title,
                    DonatedAt = d.DonatedAt
                }).ToList(),
                VolunteerApplications = volunteerings.Select(v => new VolunteerContributionDto
                {
                    RequestTitle = v.VolunteerRequest.Request.Title,
                    Status = v.Status.ToString().ToUpper(),
                    AppliedAt = v.JoinedAt
                }).ToList(),
                ResourceContributions = resources.Select(r => new ResourceContributionDto
                {
                    Quantity = r.Quantity,
                    ResourceType = r.ResourceRequest.ResourceType,
                    RequestTitle = r.ResourceRequest.Request.Title,
                    ContributedAt = r.ContributedAt
                }).ToList()
            };
        }

        private RequestResponseDto MapToDto(Request request, int? donorId = null)
        {
            RequestResponseDto dto;

            if (request.FundraiserRequest != null)
            {
                dto = new FundraiserRequestResponseDto
                {
                    TargetAmount = request.FundraiserRequest.TargetAmount,
                    CollectedAmount = request.FundraiserRequest.CollectedAmount,
                    Deadline = request.FundraiserRequest.Deadline
                };
            }
            else if (request.ResourceRequest != null)
            {
                dto = new ResourceRequestResponseDto
                {
                    ResourceType = request.ResourceRequest.ResourceType,
                    QuantityRequired = request.ResourceRequest.QuantityRequired,
                    QuantityReceived = request.ResourceRequest.QuantityReceived
                };
            }
            else if (request.VolunteerRequest != null)
            {
                var vrDto = new VolunteerRequestResponseDto
                {
                    SkillsRequired = request.VolunteerRequest.SkillsRequired,
                    VolunteersRequired = request.VolunteerRequest.VolunteersRequired,
                    VolunteerCount = request.VolunteerRequest.VolunteerParticipations?.Count ?? 0
                };

                if (donorId.HasValue && request.VolunteerRequest.VolunteerParticipations != null)
                {
                    vrDto.HasApplied = request.VolunteerRequest.VolunteerParticipations.Any(vp => vp.DonorId == donorId.Value);
                }

                dto = vrDto;
            }
            else
            {
                dto = new RequestResponseDto();
            }

            dto.RequestId = request.RequestId;
            dto.NgoId = request.NgoId;
            dto.Ngo = request.NGO != null ? new NgoDto { Name = request.NGO.Name } : null;
            dto.Title = request.Title;
            dto.Description = request.Description;
            dto.Status = request.Status.ToString().ToUpper();
            dto.RequestType = request.RequestType.ToString().ToUpper();
            dto.CreatedAt = request.CreatedAt;

            return dto;
        }
    }
}
