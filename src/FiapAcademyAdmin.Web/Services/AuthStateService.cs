using FiapAcademyAdmin.Application.Interfaces.Services;
using System.Security.Claims;

namespace FiapAcademyAdmin.Web.Services
{
    public class AuthStateService(IServiceScopeFactory serviceScopeFactory) : IAuthStateService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly Lock _lock = new();
        private string? _token;
        private ClaimsPrincipal? _currentUser;

        public event Action? AuthenticationStateChanged;

        public bool IsAuthenticated => !string.IsNullOrEmpty(_token) && ValidateToken(_token);
        public string? CurrentUser => _currentUser?.Identity?.Name;
        public string? Token => _token;

        private IMediatorAuthService GetMediatorAuthService()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IMediatorAuthService>();
        }

        private bool ValidateToken(string token)
        {
            return GetMediatorAuthService().ValidateToken(token);
        }

        public void Login(string username, string token)
        {
            lock (_lock)
            {
                _token = token;
                _currentUser = GetMediatorAuthService().GetUserFromToken(token);
            }
            
            AuthenticationStateChanged?.Invoke();
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediatorAuthService = scope.ServiceProvider.GetRequiredService<IMediatorAuthService>();
            
            var loginResponse = await mediatorAuthService.LoginAsync(username, password);
            
            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
            {
                lock (_lock)
                {
                    _token = loginResponse.Token;
                    _currentUser = mediatorAuthService.GetUserFromToken(loginResponse.Token);
                }
                
                AuthenticationStateChanged?.Invoke();
                return true;
            }
            
            return false;
        }

        public async Task<bool> RegisterAsync(string nome, string email, string senha, string confirmarSenha)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediatorAuthService = scope.ServiceProvider.GetRequiredService<IMediatorAuthService>();
            
            var success = await mediatorAuthService.RegisterAsync(nome, email, senha, confirmarSenha);
            return success;
        }

        public void Logout()
        {
            lock (_lock)
            {
                _token = null;
                _currentUser = null;
            }
            
            AuthenticationStateChanged?.Invoke();
        }

        public ClaimsPrincipal? GetCurrentUser()
        {
            lock (_lock)
            {
                if (IsAuthenticated)
                {
                    return _currentUser;
                }
                return null;
            }
        }
    }
} 