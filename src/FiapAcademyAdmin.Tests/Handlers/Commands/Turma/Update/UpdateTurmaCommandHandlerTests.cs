using FiapAcademyAdmin.Application.Handlers.Commands.Turma.Update;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Tests.TestHelpers.Turma;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Turma.Update
{
    public class UpdateTurmaCommandHandlerTests
    {
        private readonly Mock<ITurmaService> _turmaServiceMock = new();
        private readonly Mock<IMatriculaService> _matriculaServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly UpdateTurmaCommandHandler _handler;

        public UpdateTurmaCommandHandlerTests()
        {
            _handler = new UpdateTurmaCommandHandler(
                _turmaServiceMock.Object,
                _matriculaServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_DeveAtualizarTurmaComSucesso()
        {
            // Arrange
            var updateTurmaDto = TurmaTestDataBuilder.UpdateTurmaCommandDTO();
            var command = new UpdateTurmaCommand { Turma = updateTurmaDto };
            var turmaAtual = new TurmaEntity(updateTurmaDto.Nome, updateTurmaDto.Descricao);
            var turmaAtualizada = new TurmaEntity(updateTurmaDto.Nome, updateTurmaDto.Descricao);
            var turmaQueryDto = TurmaTestDataBuilder.TurmaQueryDTO();

            _turmaServiceMock.SetupTurmaServiceForUpdate(turmaAtualizada);
            _matriculaServiceMock.Setup(s => s.GetByTurmaIdAsync(updateTurmaDto.Id)).ReturnsAsync(new List<MatriculaEntity>());
            _mapperMock.SetupMapperForQueryDTO(turmaQueryDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(turmaQueryDto);
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoTurmaNaoEncontrada()
        {
            // Arrange
            var updateTurmaDto = TurmaTestDataBuilder.UpdateTurmaCommandDTO();
            var command = new UpdateTurmaCommand { Turma = updateTurmaDto };
            
            _turmaServiceMock.Setup(s => s.GetByIdAsync(updateTurmaDto.Id)).ReturnsAsync((TurmaEntity?)null);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Turma não encontrada");
            result.Data.Should().BeNull();
        }
    }
} 