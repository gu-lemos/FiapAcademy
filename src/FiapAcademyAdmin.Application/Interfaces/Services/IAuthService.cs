using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.DTOs.Query.Auth;

namespace FiapAcademyAdmin.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginCommandDTO loginDto);
        Task<bool> RegisterAsync(RegisterCommandDTO registerDto);
        bool ValidateTokenAsync(string token);
        string GenerateToken(string name, string email, string role);
    }
} 