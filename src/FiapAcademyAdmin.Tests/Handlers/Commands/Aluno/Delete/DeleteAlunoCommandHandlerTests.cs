using FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Delete;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Aluno;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Aluno.Delete
{
    public class DeleteAlunoCommandHandlerTests
    {
        private readonly Mock<IAlunoService> _alunoServiceMock = new();
        private readonly DeleteAlunoCommandHandler _handler;

        public DeleteAlunoCommandHandlerTests()
        {
            _handler = new DeleteAlunoCommandHandler(_alunoServiceMock.Object);
        }

        [Fact]
        public async Task Handle_DeveDeletarAlunoComSucesso()
        {
            // Arrange
            var alunoId = new Bogus.Faker().Random.Int(1, 1000);
            var command = new DeleteAlunoCommand { Id = alunoId };
            
            _alunoServiceMock.SetupAlunoServiceForDelete();

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoAlunoNaoEncontrado()
        {
            // Arrange
            var alunoId = new Bogus.Faker().Random.Int(1, 1000);
            var command = new DeleteAlunoCommand { Id = alunoId };
            
            _alunoServiceMock.Setup(s => s.DeleteAsync(alunoId)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Aluno não encontrado");
            result.Data.Should().BeFalse();
        }
    }
} 