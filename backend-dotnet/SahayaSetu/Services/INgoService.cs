using SahayaSetu.DTO;
using SahayaSetu.DTO.NGO;
using SahayaSetu.DTO.Request;

namespace SahayaSetu.Services
{
    public interface INgoService
    {
        Task CreateResourceRequestAsync(int ngoId, CreateResourceRequestDto dto);
        Task CreateVolunteerRequestAsync(int ngoId, CreateVolunteerRequestDto dto);
        Task CreateFundraiserRequestAsync(int ngoId, CreateFundraiserRequestDto dto);
        Task<List<RequestResponseDto>> GetNgoRequestsAsync(int ngoId);
        Task UpdateNgoProfileAsync(int ngoId, UpdateNgoDto dto);
        Task FulfillRequestAsync(int requestId); // Or CloseRequest logic
        Task CloseRequestAsync(int requestId, int ngoId);
        Task<object> GetRequestDetailsAsync(int requestId);
        Task<List<DonationResponseDto>> GetNgoDonationsAsync(int ngoId);
        Task<List<DonationResponseDto>> GetFundraiserDonationsAsync(int ngoId);
        Task SendDonationResponseAsync(DonationResponseRequestDto dto);
    }
}
