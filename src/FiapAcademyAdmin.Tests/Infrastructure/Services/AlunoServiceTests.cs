using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Services;
using FiapAcademyAdmin.Tests.TestHelpers.Aluno;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Infrastructure.Services
{
    public class AlunoServiceTests
    {
        private readonly Mock<IAlunoRepository> _repoMock = new();
        private readonly AlunoService _service;

        public AlunoServiceTests()
        {
            _service = new AlunoService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarAlunos()
        {
            var alunos = AlunoTestDataBuilder.AlunoEntities(3);
            _repoMock.Setup(r => r.GetAllAsync(1, 10, null)).ReturnsAsync(alunos);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarAluno()
        {
            var aluno = AlunoTestDataBuilder.AlunoEntity();
            _repoMock.Setup(r => r.GetByIdAsync(aluno.Id)).ReturnsAsync(aluno);

            var result = await _service.GetByIdAsync(aluno.Id);

            result.Should().Be(aluno);
        }

        [Fact]
        public async Task CreateAsync_DeveHashSenhaENovoAluno()
        {
            var aluno = AlunoTestDataBuilder.AlunoEntity();
            _repoMock.Setup(r => r.AddAsync(It.IsAny<AlunoEntity>())).ReturnsAsync((AlunoEntity a) => a);

            var result = await _service.CreateAsync(aluno);

            result.Should().NotBeNull();
            result.Senha.Should().NotBe(aluno.Senha);
        }

        [Fact]
        public async Task UpdateAsync_DeveHashSenhaSeInformada()
        {
            var aluno = AlunoTestDataBuilder.AlunoEntity();
            aluno.AtualizarSenha("NovaSenha123!");
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<AlunoEntity>())).ReturnsAsync((AlunoEntity a) => a);

            var result = await _service.UpdateAsync(aluno);

            result.Should().NotBeNull();
            result!.Senha.Should().NotBe("NovaSenha123!");
        }

        [Fact]
        public async Task DeleteAsync_DeveChamarRepositorio()
        {
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _service.DeleteAsync(1);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsByCpfAsync_DeveRetornarTrueSeExistir()
        {
            _repoMock.Setup(r => r.ExistsByCpfAsync("12345678900")).ReturnsAsync(true);

            var result = await _service.ExistsByCpfAsync("12345678900");

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsByEmailAsync_DeveRetornarTrueSeExistir()
        {
            _repoMock.Setup(r => r.ExistsByEmailAsync("teste@email.com")).ReturnsAsync(true);

            var result = await _service.ExistsByEmailAsync("teste@email.com");

            result.Should().BeTrue();
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