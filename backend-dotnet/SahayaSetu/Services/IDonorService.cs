using SahayaSetu.DTO;

namespace SahayaSetu.Services
{
    public interface IDonorService
    {
        Task<List<RequestResponseDto>> GetAllOpenRequestsAsync();
        Task<List<RequestResponseDto>> GetAllOpenRequestsForDonorAsync(int donorId);
        Task<List<FundraiserRequestResponseDto>> GetOpenFundraisersAsync();
        Task DonateToFundraiserAsync(int donorId, int requestId, DonationDto dto);
        Task ApplyForVolunteerAsync(int donorId, int requestId);
        Task FulfillResourceRequestAsync(int donorId, int requestId, int quantity);
        Task<ContributionSummaryDto> GetDonorContributionsAsync(int donorId);
    }
}
