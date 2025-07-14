using FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetAll;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Aluno;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Queries.Aluno.GetAll
{
    public class GetAlunosQueryHandlerTests
    {
        private readonly Mock<IAlunoService> _alunoServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly GetAlunosQueryHandler _handler;

        public GetAlunosQueryHandlerTests()
        {
            _handler = new GetAlunosQueryHandler(_alunoServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarListaDeAlunosComSucesso()
        {
            // Arrange
            var query = new GetAlunosQuery();
            var alunosEntities = AlunoTestDataBuilder.AlunoEntities();
            var alunosQueryDtos = AlunoTestDataBuilder.AlunoQueryDTOs();
            var totalCount = 5;
            
            _alunoServiceMock.SetupAlunoServiceForGetAll(alunosEntities, totalCount);
            _mapperMock.SetupMapperForQueryDTOs(alunosQueryDtos);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data?.Alunos.Should().BeEquivalentTo(alunosQueryDtos);
            result.Data?.TotalCount.Should().Be(totalCount);
        }
    }
} 