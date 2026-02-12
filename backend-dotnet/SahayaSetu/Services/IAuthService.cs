using SahayaSetu.DTO.Auth;
using SahayaSetu.DTO.NGO;

namespace SahayaSetu.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterDonorAsync(DonorRegisterDto dto);
        Task<AuthResponseDto> RegisterAdminAsync(AdminRegisterDto dto);
        Task<AuthResponseDto> RegisterNgoAsync(NgoRegisterDto dto);
        Task<AuthResponseDto> LoginUserAsync(LoginDto dto);
        Task<AuthResponseDto> LoginNgoAsync(LoginDto dto);
    }
}
