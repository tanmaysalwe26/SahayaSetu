using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SahayaSetu.Data;
using SahayaSetu.DTO.Auth;
using SahayaSetu.DTO.NGO;
using SahayaSetu.Helpers;
using SahayaSetu.Models;

namespace SahayaSetu.Services
{
    public class AuthService : IAuthService
    {
        private readonly MyAppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(MyAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateJwtToken(string email, string role, int id)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim("role", role),
                new Claim("id", id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_super_secret_key_which_is_ver_very_long_and_secure"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponseDto> RegisterDonorAsync(DonorRegisterDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already active");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = new User
                {
                    Email = dto.Email,
                    PasswordHash = PasswordHelper.HashPassword(dto.Password),
                    Role = UserRole.Donor
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var donor = new Donor
                {
                    UserId = user.UserId,
                    FullName = dto.FullName,
                    DateOfBirth = dto.DateOfBirth,
                    Phone = dto.Phone,
                    Address = dto.Address,
                    City = dto.City
                };

                _context.Donors.Add(donor);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var token = GenerateJwtToken(user.Email, user.Role.ToString().ToUpper(), user.UserId);
                return new AuthResponseDto
                {
                    Token = token,
                    Role = user.Role.ToString().ToUpper(),
                    Message = "Donor registered successfully"
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<AuthResponseDto> RegisterAdminAsync(AdminRegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already active");

            var admin = new User
            {
                Email = dto.Email,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                Role = UserRole.Admin
            };

            _context.Users.Add(admin);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(admin.Email, admin.Role.ToString().ToUpper(), admin.UserId);
            return new AuthResponseDto
            {
                Token = token,
                Role = admin.Role.ToString().ToUpper(),
                Message = "Admin registered successfully"
            };
        }

        public async Task<AuthResponseDto> RegisterNgoAsync(NgoRegisterDto dto)
        {
            if (await _context.NGOs.AnyAsync(n => n.Email == dto.Email))
                throw new Exception("Email already registered");
            
            if (await _context.NGOs.AnyAsync(n => n.DarpanId == dto.DarpanId))
                throw new Exception("Darpan ID already registered");

            var ngo = new NGO
            {
                Email = dto.Email,
                PasswordHash = PasswordHelper.HashPassword(dto.Password),
                Name = dto.Name,
                DarpanId = dto.DarpanId,
                ContactPhone = dto.ContactPhone,
                Address = dto.Address,
                City = dto.City,
                Status = NgoStatus.Pending
            };

            _context.NGOs.Add(ngo);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = null,
                Role = "NGO",
                Message = "NGO registration successful. Please wait for Admin approval."
            };
        }

        public async Task<AuthResponseDto> LoginUserAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("User not found");
            
            // Check if user is active (matching Java backend)
            // Assuming we need to add IsActive property or check existing logic

            int tokenPayloadId = user.UserId;
            string userName = "Admin";

            // If Donor, fetch DonorId
            if (user.Role == UserRole.Donor)
            {
                var donor = await _context.Donors.FirstOrDefaultAsync(d => d.UserId == user.UserId);
                if (donor == null) throw new Exception("Login failed: User account exists but Donor profile is missing. Please register again.");
                tokenPayloadId = donor.DonorId; // Match Java behavior
                userName = donor.FullName;
            }

            var token = GenerateJwtToken(user.Email, user.Role.ToString().ToUpper(), tokenPayloadId);

            return new AuthResponseDto
            {
                Token = token,
                Role = user.Role.ToString().ToUpper(),
                Name = userName,
                Id = user.Role == UserRole.Donor ? tokenPayloadId : user.UserId,
                Message = "Login successful"
            };
        }

        public async Task<AuthResponseDto> LoginNgoAsync(LoginDto dto)
        {
            var ngo = await _context.NGOs
               .FirstOrDefaultAsync(n => n.Email == dto.Email);

            if (ngo == null || !PasswordHelper.VerifyPassword(dto.Password, ngo.PasswordHash))
                throw new Exception("NGO not found");

            if (ngo.Status != NgoStatus.Approved)
                throw new Exception($"NGO account is not approved yet. Current status: {ngo.Status}");

            var token = GenerateJwtToken(ngo.Email, "NGO", ngo.NgoId);

            return new AuthResponseDto
            {
                Token = token,
                Role = "NGO",
                Id = ngo.NgoId,
                Name = ngo.Name,
                Message = "Login successful"
            };
        }
    }
}
