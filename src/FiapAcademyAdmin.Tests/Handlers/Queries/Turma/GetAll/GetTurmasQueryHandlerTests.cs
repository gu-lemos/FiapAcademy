using FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAll;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Turma;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Queries.Turma.GetAll
{
    public class GetTurmasQueryHandlerTests
    {
        private readonly Mock<ITurmaService> _turmaServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly GetTurmasQueryHandler _handler;

        public GetTurmasQueryHandlerTests()
        {
            _handler = new GetTurmasQueryHandler(_turmaServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaDeTurmasComSucesso()
        {
            // Arrange
            var query = new GetTurmasQuery();
            var turmasEntities = TurmaTestDataBuilder.TurmaEntities();
            var turmasQueryDtos = TurmaTestDataBuilder.TurmaQueryDTOs();
            var totalCount = 5;
            
            _turmaServiceMock.SetupTurmaServiceForGetAll(turmasEntities, totalCount);
            _mapperMock.SetupMapperForQueryDTOs(turmasQueryDtos);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data?.Turmas.Should().BeEquivalentTo(turmasQueryDtos);
            result.Data?.TotalCount.Should().Be(totalCount);
        }
    }
} 