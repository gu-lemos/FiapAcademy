using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.DTOs.Query.Auth;
using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FiapAcademyAdmin.Infrastructure.Services
{
    public class AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration) : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<LoginResponseDTO> LoginAsync(LoginCommandDTO loginDto)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);
            
            if (usuario == null || !usuario.Ativo)
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            var senhaHash = HashPassword(loginDto.Senha);
            if (usuario.Senha != senhaHash)
            {
                throw new UnauthorizedAccessException("Email ou senha inválidos");
            }

            var token = GenerateToken(usuario.Nome, usuario.Email, usuario.Role);
            var expiraEm = DateTime.UtcNow.AddHours(24);

            return new LoginResponseDTO
            {
                Token = token,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Role = usuario.Role,
                ExpiraEm = expiraEm
            };
        }

        public async Task<bool> RegisterAsync(RegisterCommandDTO registerDto)
        {
            if (registerDto.Senha != registerDto.ConfirmarSenha)
            {
                throw new ArgumentException("As senhas não coincidem");
            }

            if (await _usuarioRepository.ExistsByEmailAsync(registerDto.Email))
            {
                throw new ArgumentException("Email já cadastrado");
            }

            var senhaHash = HashPassword(registerDto.Senha);
            var usuario = new UsuarioEntity(registerDto.Nome, registerDto.Email, senhaHash);

            await _usuarioRepository.AddAsync(usuario);
            return true;
        }

        public bool ValidateTokenAsync(string token)
        {
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

        public string GenerateToken(string nome, string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret não configurado"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 