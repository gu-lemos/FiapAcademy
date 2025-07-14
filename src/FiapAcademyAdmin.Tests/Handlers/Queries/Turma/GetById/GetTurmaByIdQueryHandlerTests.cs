using Bogus;
using FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetById;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Tests.TestHelpers.Turma;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Queries.Turma.GetById
{
    public class GetTurmaByIdQueryHandlerTests
    {
        private readonly Mock<ITurmaService> _turmaServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly GetTurmaByIdQueryHandler _handler;

        public GetTurmaByIdQueryHandlerTests()
        {
            _handler = new GetTurmaByIdQueryHandler(_turmaServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarTurmaComSucesso()
        {
            // Arrange
            var turmaId = new Faker().Random.Int(1, 1000);
            var query = new GetTurmaByIdQuery { Id = turmaId };
            var turmaEntity = TurmaTestDataBuilder.TurmaEntity();
            var turmaQueryDto = TurmaTestDataBuilder.TurmaQueryDTO();
            
            _turmaServiceMock.SetupTurmaServiceForGetById(turmaEntity);
            _mapperMock.SetupMapperForQueryDTO(turmaQueryDto);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(turmaQueryDto);
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoTurmaNaoEncontrada()
        {
            // Arrange
            var turmaId = new Faker().Random.Int(1, 1000);
            var query = new GetTurmaByIdQuery { Id = turmaId };
            
            _turmaServiceMock.Setup(s => s.GetByIdAsync(turmaId)).ReturnsAsync((TurmaEntity?)null);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Turma não encontrada");
            result.Data.Should().BeNull();
        }
    }
} 