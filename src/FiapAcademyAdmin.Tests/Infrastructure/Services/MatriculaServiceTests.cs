using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Infrastructure.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Matricula;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Infrastructure.Services
{
    public class MatriculaServiceTests
    {
        private readonly Mock<IMatriculaRepository> _repoMock = new();
        private readonly MatriculaService _service;

        public MatriculaServiceTests()
        {
            _service = new MatriculaService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarMatriculas()
        {
            var matriculas = MatriculaTestDataBuilder.MatriculaEntities(3);
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(matriculas);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarMatricula()
        {
            var matricula = MatriculaTestDataBuilder.MatriculaEntity();
            _repoMock.Setup(r => r.GetByIdAsync(matricula.Id)).ReturnsAsync(matricula);

            var result = await _service.GetByIdAsync(matricula.Id);

            result.Should().Be(matricula);
        }

        [Fact]
        public async Task GetByAlunoIdAsync_DeveRetornarMatriculasDoAluno()
        {
            var matriculas = MatriculaTestDataBuilder.MatriculaEntities(2, alunoId: 5);
            _repoMock.Setup(r => r.GetByAlunoIdAsync(5)).ReturnsAsync(matriculas);

            var result = await _service.GetByAlunoIdAsync(5);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByTurmaIdAsync_DeveRetornarMatriculasDaTurma()
        {
            var matriculas = MatriculaTestDataBuilder.MatriculaEntities(2, alunoId: 1, turmaIdStart: 10);
            _repoMock.Setup(r => r.GetByTurmaIdAsync(10)).ReturnsAsync(matriculas);

            var result = await _service.GetByTurmaIdAsync(10);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByAlunoAndTurmaAsync_DeveRetornarMatriculaCorreta()
        {
            var matricula = MatriculaTestDataBuilder.MatriculaEntity(2, 3);
            _repoMock.Setup(r => r.GetByAlunoAndTurmaAsync(2, 3)).ReturnsAsync(matricula);

            var result = await _service.GetByAlunoAndTurmaAsync(2, 3);

            result.Should().Be(matricula);
        }

        [Fact]
        public async Task ExistsByAlunoAndTurmaAsync_DeveRetornarTrueSeExistir()
        {
            _repoMock.Setup(r => r.ExistsByAlunoAndTurmaAsync(2, 3)).ReturnsAsync(true);

            var result = await _service.ExistsByAlunoAndTurmaAsync(2, 3);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAsync_DeveAdicionarMatriculaSeValidaENaoExistente()
        {
            var matricula = MatriculaTestDataBuilder.MatriculaEntity(2, 3);
            _repoMock.Setup(r => r.ExistsByAlunoAndTurmaAsync(2, 3)).ReturnsAsync(false);
            _repoMock.Setup(r => r.AddAsync(matricula)).ReturnsAsync(matricula);

            var result = await _service.CreateAsync(matricula);

            result.Should().Be(matricula);
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecaoSeMatriculaInvalida()
        {
            var matricula = MatriculaTestDataBuilder.MatriculaEntity(0, 0);

            Func<Task> act = async () => await _service.CreateAsync(matricula);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Dados da matrícula são inválidos.");
        }

        [Fact]
        public async Task CreateAsync_DeveLancarExcecaoSeMatriculaJaExiste()
        {
            var matricula = MatriculaTestDataBuilder.MatriculaEntity(2, 3);
            _repoMock.Setup(r => r.ExistsByAlunoAndTurmaAsync(2, 3)).ReturnsAsync(true);

            Func<Task> act = async () => await _service.CreateAsync(matricula);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Aluno já está matriculado nesta turma.");
        }

        [Fact]
        public async Task DeleteAsync_DeveChamarRepositorio()
        {
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _service.DeleteAsync(1);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteByAlunoAndTurmaAsync_DeveChamarRepositorio()
        {
            _repoMock.Setup(r => r.DeleteByAlunoAndTurmaAsync(2, 3)).ReturnsAsync(true);

            var result = await _service.DeleteByAlunoAndTurmaAsync(2, 3);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetCountByTurmaIdAsync_DeveRetornarQuantidade()
        {
            _repoMock.Setup(r => r.GetCountByTurmaIdAsync(10)).ReturnsAsync(7);

            var result = await _service.GetCountByTurmaIdAsync(10);

            result.Should().Be(7);
        }
    }
} 