using Bogus;
using FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetById;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Tests.TestHelpers.Aluno;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Queries.Aluno.GetById
{
    public class GetAlunoByIdQueryHandlerTests
    {
        private readonly Mock<IAlunoService> _alunoServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly GetAlunoByIdQueryHandler _handler;

        public GetAlunoByIdQueryHandlerTests()
        {
            _handler = new GetAlunoByIdQueryHandler(_alunoServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarAlunoComSucesso()
        {
            // Arrange
            var alunoId = new Faker().Random.Int(1, 1000);
            var query = new GetAlunoByIdQuery { Id = alunoId };
            var alunoEntity = AlunoTestDataBuilder.AlunoEntity();
            var alunoQueryDto = AlunoTestDataBuilder.AlunoQueryDTO();
            
            _alunoServiceMock.SetupAlunoServiceForGetById(alunoEntity);
            _mapperMock.SetupMapperForQueryDTO(alunoQueryDto);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(alunoQueryDto);
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoAlunoNaoEncontrado()
        {
            // Arrange
            var alunoId = new Faker().Random.Int(1, 1000);
            var query = new GetAlunoByIdQuery { Id = alunoId };
            
            _alunoServiceMock.Setup(s => s.GetByIdAsync(alunoId)).ReturnsAsync((AlunoEntity?)null);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Aluno não encontrado");
            result.Data.Should().BeNull();
        }
    }
} 