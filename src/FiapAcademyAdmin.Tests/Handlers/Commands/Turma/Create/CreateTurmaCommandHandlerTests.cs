using FiapAcademyAdmin.Application.Handlers.Commands.Turma.Create;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Tests.TestHelpers.Turma;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Turma.Create
{
    public class CreateTurmaCommandHandlerTests
    {
        private readonly Mock<ITurmaService> _turmaServiceMock = new();
        private readonly Mock<IMatriculaService> _matriculaServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly CreateTurmaCommandHandler _handler;

        public CreateTurmaCommandHandlerTests()
        {
            _handler = new CreateTurmaCommandHandler(
                _turmaServiceMock.Object,
                _matriculaServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_DeveCriarTurmaComSucesso()
        {
            // Arrange
            var createTurmaDto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            var command = new CreateTurmaCommand { Turma = createTurmaDto };
            var turmaEntity = new TurmaEntity(createTurmaDto.Nome, createTurmaDto.Descricao);
            var turmaQueryDto = TurmaTestDataBuilder.TurmaQueryDTO();

            _turmaServiceMock.SetupTurmaServiceForCreate(turmaEntity);
            _matriculaServiceMock.Setup(s => s.CreateAsync(It.IsAny<MatriculaEntity>())).ReturnsAsync(new MatriculaEntity(1, 1));
            _mapperMock.SetupMapperForQueryDTO(turmaQueryDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(turmaQueryDto);
        }
    }
} 