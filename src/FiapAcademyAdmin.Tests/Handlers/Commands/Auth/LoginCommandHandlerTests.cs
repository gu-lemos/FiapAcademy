using FiapAcademyAdmin.Application.Handlers.Commands.Auth.Login;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Auth;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Auth
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
        {
            _handler = new LoginCommandHandler(_authServiceMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarLoginResponseDTO_QuandoLoginForValido()
        {
            // Arrange
            var loginDto = AuthTestDataBuilder.LoginCommandDTO();
            var command = new LoginCommand(loginDto);
            var expectedResponse = AuthTestDataBuilder.LoginResponseDTO();

            _authServiceMock.Setup(s => s.LoginAsync(loginDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().Be(expectedResponse.Token);
            result.Email.Should().Be(expectedResponse.Email);
            result.Nome.Should().Be(expectedResponse.Nome);
            result.Role.Should().Be(expectedResponse.Role);
        }

        [Fact]
        public async Task Handle_DeveRetornarLoginResponseDTOComTokenVazio_QuandoLoginForInvalido()
        {
            // Arrange
            var loginDto = AuthTestDataBuilder.LoginCommandDTO(email: "invalido@teste.com", senha: "errada");
            var command = new LoginCommand(loginDto);
            var emptyResponse = AuthTestDataBuilder.LoginResponseDTO(token: "");

            _authServiceMock.Setup(s => s.LoginAsync(loginDto)).ReturnsAsync(emptyResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().BeEmpty();
        }
    }
} 