using FiapAcademyAdmin.Application.Handlers.Commands.Auth.Register;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Auth;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Auth
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IAuthService> _authServiceMock = new();
        private readonly RegisterCommandHandler _handler;

        public RegisterCommandHandlerTests()
        {
            _handler = new RegisterCommandHandler(_authServiceMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarTrue_QuandoRegistroForBemSucedido()
        {
            // Arrange
            var registerDto = AuthTestDataBuilder.RegisterCommandDTO();
            var command = new RegisterCommand(registerDto);

            _authServiceMock.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_DeveRetornarFalse_QuandoRegistroFalhar()
        {
            // Arrange
            var registerDto = AuthTestDataBuilder.RegisterCommandDTO(email: "jaexiste@teste.com");
            var command = new RegisterCommand(registerDto);

            _authServiceMock.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
        }
    }
} 