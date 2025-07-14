using Bogus;
using FiapAcademyAdmin.Application.Handlers.Commands.Turma.Delete;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Tests.TestHelpers.Turma;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Turma.Delete
{
    public class DeleteTurmaCommandHandlerTests
    {
        private readonly Mock<ITurmaService> _turmaServiceMock = new();
        private readonly DeleteTurmaCommandHandler _handler;

        public DeleteTurmaCommandHandlerTests()
        {
            _handler = new DeleteTurmaCommandHandler(_turmaServiceMock.Object);
        }

        [Fact]
        public async Task Handle_DeveDeletarTurmaComSucesso()
        {
            // Arrange
            var turmaId = new Faker().Random.Int(1, 1000);
            var command = new DeleteTurmaCommand { Id = turmaId };
            var turmaEntity = TurmaTestDataBuilder.TurmaEntity();
            
            _turmaServiceMock.SetupTurmaServiceForGetById(turmaEntity);
            _turmaServiceMock.SetupTurmaServiceForDelete();

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoTurmaNaoEncontrada()
        {
            // Arrange
            var turmaId = new Faker().Random.Int(1, 1000);
            var command = new DeleteTurmaCommand { Id = turmaId };
            
            _turmaServiceMock.Setup(s => s.GetByIdAsync(turmaId)).ReturnsAsync((TurmaEntity?)null);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Turma não encontrada");
            result.Data.Should().BeFalse();
        }
    }
} 