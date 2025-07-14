using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.DTOs.Query.Auth;
using FiapAcademyAdmin.Application.Handlers.Commands.Auth.Login;
using FiapAcademyAdmin.Application.Handlers.Commands.Auth.Register;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapAcademyAdmin.Web.Services
{
    public interface IMediatorAuthService
    {
        Task<LoginResponseDTO?> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string nome, string email, string senha, string confirmarSenha);
        bool ValidateToken(string token);
        ClaimsPrincipal? GetUserFromToken(string token);
    }

    public class MediatorAuthService(IMediator mediator, IConfiguration configuration) : IMediatorAuthService
    {
        private readonly IMediator _mediator = mediator;
        private readonly IConfiguration _configuration = configuration;

        public async Task<LoginResponseDTO?> LoginAsync(string username, string password)
        {
            var loginDto = new LoginCommandDTO
            {
                Email = username,
                Senha = password
            };

            var command = new LoginCommand(loginDto);
            var result = await _mediator.Send(command);
            
            return result;
        }

        public async Task<bool> RegisterAsync(string nome, string email, string senha, string confirmarSenha)
        {
            var registerDto = new RegisterCommandDTO
            {
                Nome = nome,
                Email = email,
                Senha = senha,
                ConfirmarSenha = confirmarSenha
            };

            var command = new RegisterCommand(registerDto);
            var result = await _mediator.Send(command);
            
            return result;
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret não configurado"));

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }

        public ClaimsPrincipal? GetUserFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret não configurado"));
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return principal;
        }
    }
} 