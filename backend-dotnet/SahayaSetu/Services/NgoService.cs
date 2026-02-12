using Microsoft.EntityFrameworkCore;
using SahayaSetu.Data;
using SahayaSetu.DTO;
using SahayaSetu.DTO.NGO;
using SahayaSetu.DTO.Request;
using SahayaSetu.Models;

namespace SahayaSetu.Services
{
    public class NgoService : INgoService
    {
        private readonly MyAppDbContext _context;

        public NgoService(MyAppDbContext context)
        {
            _context = context;
        }

        private async Task<NGO> GetNgoIfApprovedAsync(int ngoId)
        {
            var ngo = await _context.NGOs.FindAsync(ngoId)
                ?? throw new Exception("NGO not found");

            if (ngo.Status != NgoStatus.Approved)
                throw new Exception("NGO is not approved to create requests");

            return ngo;
        }

        public async Task CreateResourceRequestAsync(int ngoId, CreateResourceRequestDto dto)
        {
            await GetNgoIfApprovedAsync(ngoId);

            var request = new Request
            {
                NgoId = ngoId,
                Title = dto.Title,
                Description = dto.Description,
                RequestType = RequestType.Resource,
                Status = RequestStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var resource = new ResourceRequest
            {
                RequestId = request.RequestId,
                ResourceType = dto.ResourceType,
                QuantityRequired = dto.QuantityRequired,
                QuantityReceived = 0
            };

            _context.ResourceRequests.Add(resource);
            await _context.SaveChangesAsync();
        }

        public async Task CreateVolunteerRequestAsync(int ngoId, CreateVolunteerRequestDto dto)
        {
            await GetNgoIfApprovedAsync(ngoId);

            var request = new Request
            {
                NgoId = ngoId,
                Title = dto.Title,
                Description = dto.Description,
                RequestType = RequestType.Volunteer,
                Status = RequestStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var volunteer = new VolunteerRequest
            {
                RequestId = request.RequestId,
                SkillsRequired = dto.SkillsRequired,
                VolunteersRequired = dto.VolunteersRequired
            };

            _context.VolunteerRequests.Add(volunteer);
            await _context.SaveChangesAsync();
        }

        public async Task CreateFundraiserRequestAsync(int ngoId, CreateFundraiserRequestDto dto)
        {
            await GetNgoIfApprovedAsync(ngoId);

            var request = new Request
            {
                NgoId = ngoId,
                Title = dto.Title,
                Description = dto.Description,
                RequestType = RequestType.Fundraiser,
                Status = RequestStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var fundraiser = new FundraiserRequest
            {
                RequestId = request.RequestId,
                TargetAmount = dto.TargetAmount,
                Deadline = dto.Deadline ?? DateTime.Now.AddDays(30),
                CollectedAmount = 0
            };

            _context.FundraiserRequests.Add(fundraiser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RequestResponseDto>> GetNgoRequestsAsync(int ngoId)
        {
            var requests = await _context.Requests
                .Include(r => r.NGO)
                .Include(r => r.ResourceRequest)
                .Include(r => r.VolunteerRequest)
                .Include(r => r.FundraiserRequest)
                .Where(r => r.NgoId == ngoId)
                .ToListAsync();

            return requests.Select(r => MapToDto(r)).ToList();
        }

        public async Task UpdateNgoProfileAsync(int ngoId, UpdateNgoDto dto)
        {
            var ngo = await _context.NGOs.FindAsync(ngoId)
                ?? throw new Exception("NGO not found");

            if (ngo.Status != NgoStatus.Approved)
                throw new Exception("Only approved NGOs can update details");

            ngo.Name = dto.Name;
            ngo.ContactPhone = dto.ContactPhone;
            ngo.Address = dto.Address;
            ngo.City = dto.City;

            await _context.SaveChangesAsync();
        }

        public async Task FulfillRequestAsync(int requestId)
        {
            var request = await _context.Requests.FindAsync(requestId)
                ?? throw new Exception("Request not found");

            request.Status = RequestStatus.Fulfilled;
            await _context.SaveChangesAsync();
        }

        public async Task CloseRequestAsync(int requestId, int ngoId)
        {
            var request = await _context.Requests.FindAsync(requestId)
                ?? throw new Exception("Request not found");

            if (request.NgoId != ngoId)
                throw new Exception("Unauthorized to close this request");

            // Java uses CLOSED, but here we only have Open/Fulfilled in enum.
            // Assuming intended to mark as Fulfilled (or need Closed status). 
            if (request.Status == RequestStatus.Closed)
                throw new Exception("Request is already closed");

            request.Status = RequestStatus.Closed; 
            await _context.SaveChangesAsync();
        }

        public async Task<object> GetRequestDetailsAsync(int requestId)
        {
            var request = await _context.Requests
                .Include(r => r.VolunteerRequest).ThenInclude(vr => vr.VolunteerParticipations).ThenInclude(vp => vp.Donor).ThenInclude(d => d.User)
                .Include(r => r.ResourceRequest) // Explicit loading for contributions below
                .Include(r => r.FundraiserRequest)
                .FirstOrDefaultAsync(r => r.RequestId == requestId)
                ?? throw new Exception("Request not found");
            
            var details = new RequestDetailsDto();

            if (request.VolunteerRequest != null)
            {
                details.Applicants = request.VolunteerRequest.VolunteerParticipations.Select(p => new ApplicantDetailDto
                {
                    DonorName = p.Donor.FullName,
                    DonorEmail = p.Donor.User.Email,
                    AppliedAt = p.JoinedAt, // Assuming JoinedAt exists on Participation
                    Status = p.Status.ToString().ToUpper()
                }).ToList();
            }
            else if (request.ResourceRequest != null)
            {
                 var contributions = await _context.ResourceContributions
                    .Include(rc => rc.Donor).ThenInclude(d => d.User)
                    .Where(rc => rc.ResourceRequestId == requestId)
                    .ToListAsync();

                details.Contributors = contributions.Select(c => new ContributorDetailDto
                {
                    DonorName = c.Donor.FullName,
                    DonorEmail = c.Donor.User.Email,
                    Quantity = c.Quantity,
                    ContributedAt = c.ContributedAt
                }).ToList();
            }
            else if (request.FundraiserRequest != null)
            {
                var donations = await _context.Donations
                    .Include(d => d.Donor).ThenInclude(d => d.User)
                    .Where(d => d.FundraiserRequestId == requestId)
                    .ToListAsync();

                details.Donors = donations.Select(d => new DonorDetailDto
                {
                    DonorName = d.Donor.FullName,
                    DonorEmail = d.Donor.User.Email,
                    Amount = d.Amount,
                    DonatedAt = d.DonatedAt
                }).ToList();
            }

            return details;
        }

        public async Task<List<DonationResponseDto>> GetNgoDonationsAsync(int ngoId)
        {
             // Join Donations via FundraiserRequest -> Request -> NgoId
             var donations = await _context.Donations
                .Include(d => d.FundraiserRequest).ThenInclude(fr => fr.Request)
                .Include(d => d.Donor).ThenInclude(don => don.User)
                .Where(d => d.FundraiserRequest.Request.NgoId == ngoId)
                .ToListAsync();

             return donations.Select(d => new DonationResponseDto
             {
                 DonationId = d.DonationId,
                 DonorName = d.Donor.FullName,
                 DonorEmail = d.Donor.User.Email,
                 Amount = d.Amount,
                 FundraiserTitle = d.FundraiserRequest.Request.Title,
                 DonationDate = d.DonatedAt,
                 ResponseStatus = d.ResponseStatus,
                 ResponseMessage = d.ResponseMessage
             }).ToList();
        }

        public async Task<List<DonationResponseDto>> GetFundraiserDonationsAsync(int ngoId)
        {
            return await GetNgoDonationsAsync(ngoId);
        }

        public async Task SendDonationResponseAsync(DonationResponseRequestDto dto)
        {
            var donation = await _context.Donations.FindAsync(dto.DonationId)
                ?? throw new Exception("Donation not found");

            donation.ResponseStatus = true;
            donation.ResponseMessage = dto.Message;
            donation.ResponseSentAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        private RequestResponseDto MapToDto(Request request)
        {
             // Reusing similar logic as DonorService but tailored for NGO response or generic
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
                 var participationCount = request.VolunteerRequest.VolunteerParticipations?.Count ?? 
                     _context.Entry(request.VolunteerRequest).Collection(vr => vr.VolunteerParticipations).Query().Count();

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
