using SahayaSetu.DTO;

namespace SahayaSetu.Services
{
    public interface IAdminService
    {
        Task<List<NgoResponseDto>> GetAllNgosAsync();
        Task<List<DonorResponseDto>> GetAllDonorsAsync();
        Task<List<RequestResponseDto>> GetAllRequestsAsync();
        Task ApproveNgoAsync(int ngoId);
        Task DisapproveNgoAsync(int ngoId); // Reject
        Task DisableNgoAsync(int ngoId);
        Task EnableNgoAsync(int ngoId);
    }
}
