using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Usuario;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FiapAcademyAdmin.Tests.Infrastructure.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock = new();
        private readonly Mock<IConfiguration> _configMock = new();
        private readonly AuthService _service;

        public AuthServiceTests()
        {
            _configMock.Setup(c => c["Jwt:Secret"]).Returns("supersecretkey12345678901234567890");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("issuer");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("audience");
            _service = new AuthService(_repoMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task LoginAsync_DeveRetornarToken_QuandoCredenciaisValidas()
        {
            var senha = "Senha123!";
            var senhaHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(senha)));
            var usuario = UsuarioTestDataBuilder.UsuarioEntity(senha: senhaHash);
            _repoMock.Setup(r => r.GetByEmailAsync(usuario.Email)).ReturnsAsync(usuario);

            var loginDto = new LoginCommandDTO { Email = usuario.Email, Senha = senha };
            var result = await _service.LoginAsync(loginDto);

            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
            result.Email.Should().Be(usuario.Email);
        }

        [Fact]
        public async Task LoginAsync_DeveLancarExcecao_QuandoUsuarioNaoExiste()
        {
            _repoMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((UsuarioEntity?)null);
            var loginDto = new LoginCommandDTO { Email = "naoexiste@email.com", Senha = "Senha123!" };

            Func<Task> act = async () => await _service.LoginAsync(loginDto);
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task LoginAsync_DeveLancarExcecao_QuandoUsuarioInativo()
        {
            var usuario = UsuarioTestDataBuilder.UsuarioEntity();
            typeof(UsuarioEntity).GetProperty("Ativo")!.SetValue(usuario, false);
            _repoMock.Setup(r => r.GetByEmailAsync(usuario.Email)).ReturnsAsync(usuario);
            var loginDto = new LoginCommandDTO { Email = usuario.Email, Senha = "Senha123!" };

            Func<Task> act = async () => await _service.LoginAsync(loginDto);
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task LoginAsync_DeveLancarExcecao_QuandoSenhaIncorreta()
        {
            var usuario = UsuarioTestDataBuilder.UsuarioEntity(senha: "hasherrado");
            _repoMock.Setup(r => r.GetByEmailAsync(usuario.Email)).ReturnsAsync(usuario);
            var loginDto = new LoginCommandDTO { Email = usuario.Email, Senha = "Senha123!" };

            Func<Task> act = async () => await _service.LoginAsync(loginDto);
            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Fact]
        public async Task RegisterAsync_DeveRegistrarUsuario_QuandoDadosValidos()
        {
            var registerDto = new RegisterCommandDTO { Nome = "Teste", Email = "novo@email.com", Senha = "Senha123!", ConfirmarSenha = "Senha123!" };
            _repoMock.Setup(r => r.ExistsByEmailAsync(registerDto.Email)).ReturnsAsync(false);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<UsuarioEntity>())).ReturnsAsync((UsuarioEntity u) => u);

            var result = await _service.RegisterAsync(registerDto);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterAsync_DeveLancarExcecao_QuandoSenhasDiferentes()
        {
            var registerDto = new RegisterCommandDTO { Nome = "Teste", Email = "novo@email.com", Senha = "Senha123!", ConfirmarSenha = "OutraSenha" };

            Func<Task> act = async () => await _service.RegisterAsync(registerDto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("As senhas não coincidem");
        }

        [Fact]
        public async Task RegisterAsync_DeveLancarExcecao_QuandoEmailJaCadastrado()
        {
            var registerDto = new RegisterCommandDTO { Nome = "Teste", Email = "existe@email.com", Senha = "Senha123!", ConfirmarSenha = "Senha123!" };
            _repoMock.Setup(r => r.ExistsByEmailAsync(registerDto.Email)).ReturnsAsync(true);

            Func<Task> act = async () => await _service.RegisterAsync(registerDto);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Email já cadastrado");
        }

        [Fact]
        public void ValidateTokenAsync_DeveRetornarTrueParaTokenValido()
        {
            var token = _service.GenerateToken("Nome", "email@email.com", "Admin");
            var result = _service.ValidateTokenAsync(token);
            result.Should().BeTrue();
        }

        [Fact]
        public void GenerateToken_DeveRetornarTokenNaoNulo()
        {
            var token = _service.GenerateToken("Nome", "email@email.com", "Admin");
            token.Should().NotBeNullOrEmpty();
        }
    }
} 