using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Turma;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Infrastructure.Services
{
    public class TurmaServiceTests
    {
        private readonly Mock<ITurmaRepository> _repoMock = new();
        private readonly TurmaService _service;

        public TurmaServiceTests()
        {
            _service = new TurmaService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTurmas()
        {
            var turmas = TurmaTestDataBuilder.TurmaEntities(3);
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(turmas);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetAllAsync_Paginado_DeveRetornarTurmas()
        {
            var turmas = TurmaTestDataBuilder.TurmaEntities(2);
            _repoMock.Setup(r => r.GetAllAsync(1, 2, null)).ReturnsAsync(turmas);

            var result = await _service.GetAllAsync(1, 2, null);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarTurma()
        {
            var turma = TurmaTestDataBuilder.TurmaEntity();
            _repoMock.Setup(r => r.GetByIdAsync(turma.Id)).ReturnsAsync(turma);

            var result = await _service.GetByIdAsync(turma.Id);

            result.Should().Be(turma);
        }

        [Fact]
        public async Task CreateAsync_DeveAdicionarTurma()
        {
            var turma = TurmaTestDataBuilder.TurmaEntity();
            _repoMock.Setup(r => r.CreateAsync(It.IsAny<TurmaEntity>())).ReturnsAsync((TurmaEntity t) => t);

            var result = await _service.CreateAsync(turma);

            result.Should().NotBeNull();
            result.Nome.Should().Be(turma.Nome);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarTurma()
        {
            var turma = TurmaTestDataBuilder.TurmaEntity();
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<TurmaEntity>())).ReturnsAsync((TurmaEntity t) => t);

            var result = await _service.UpdateAsync(turma);

            result.Should().NotBeNull();
            result!.Nome.Should().Be(turma.Nome);
        }

        [Fact]
        public async Task DeleteAsync_DeveChamarRepositorio()
        {
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            await _service.DeleteAsync(1);

            _repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetTotalCountAsync_DeveRetornarQuantidade()
        {
            _repoMock.Setup(r => r.GetTotalCountAsync(null)).ReturnsAsync(5);

            var result = await _service.GetTotalCountAsync();

            result.Should().Be(5);
        }
    }
} 