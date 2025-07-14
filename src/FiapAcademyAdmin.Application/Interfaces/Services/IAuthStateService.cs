using System.Security.Claims;

namespace FiapAcademyAdmin.Application.Interfaces.Services
{
    public interface IAuthStateService
    {
        event Action? AuthenticationStateChanged;
        bool IsAuthenticated { get; }
        string? CurrentUser { get; }
        string? Token { get; }
        Task<bool> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string nome, string email, string senha, string confirmarSenha);
        void Login(string username, string token);
        void Logout();
        ClaimsPrincipal? GetCurrentUser();
    }
}
