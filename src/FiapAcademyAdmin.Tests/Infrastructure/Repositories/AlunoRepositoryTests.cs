using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;
using FiapAcademyAdmin.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FiapAcademyAdmin.Tests.Infrastructure.Repositories
{
    public class AlunoRepositoryTests
    {
        private static ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarAluno()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            var aluno = new AlunoEntity("Teste", new DateTime(2000, 1, 1), "12345678900", "teste@email.com", "Senha123!");

            var result = await repo.AddAsync(aluno);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            (await context.Alunos.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarAlunoExistente()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            var aluno = new AlunoEntity("Teste", new DateTime(2000, 1, 1), "12345678900", "teste@email.com", "Senha123!");
            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();

            var result = await repo.GetByIdAsync(aluno.Id);

            result.Should().NotBeNull();
            result!.Nome.Should().Be("Teste");
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodosAlunos()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            context.Alunos.Add(new AlunoEntity("Aluno1", new DateTime(2000, 1, 1), "11111111111", "a1@email.com", "Senha123!"));
            context.Alunos.Add(new AlunoEntity("Aluno2", new DateTime(2000, 1, 1), "22222222222", "a2@email.com", "Senha123!"));
            await context.SaveChangesAsync();

            var result = await repo.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarAluno()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            var aluno = new AlunoEntity("Teste", new DateTime(2000, 1, 1), "12345678900", "teste@email.com", "Senha123!");
            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();

            aluno.Atualizar("NovoNome", aluno.DataNascimento, aluno.Cpf, aluno.Email, "NovaSenha123!");
            var result = await repo.UpdateAsync(aluno);

            result.Should().NotBeNull();
            result!.Nome.Should().Be("NovoNome");
            result.Senha.Should().Be("NovaSenha123!");
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverAluno()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            var aluno = new AlunoEntity("Teste", new DateTime(2000, 1, 1), "12345678900", "teste@email.com", "Senha123!");
            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();

            var result = await repo.DeleteAsync(aluno.Id);

            result.Should().BeTrue();
            (await context.Alunos.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task ExistsByCpfAsync_DeveRetornarTrueSeCpfExistir()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            var aluno = new AlunoEntity("Teste", new DateTime(2000, 1, 1), "12345678900", "teste@email.com", "Senha123!");
            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();

            var result = await repo.ExistsByCpfAsync("12345678900");
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsByEmailAsync_DeveRetornarTrueSeEmailExistir()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            var aluno = new AlunoEntity("Teste", new DateTime(2000, 1, 1), "12345678900", "teste@email.com", "Senha123!");
            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();

            var result = await repo.ExistsByEmailAsync("teste@email.com");
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetTotalCountAsync_DeveRetornarQuantidadeCorreta()
        {
            using var context = CreateInMemoryContext();
            var repo = new AlunoRepository(context);
            context.Alunos.Add(new AlunoEntity("Aluno1", new DateTime(2000, 1, 1), "11111111111", "a1@email.com", "Senha123!"));
            context.Alunos.Add(new AlunoEntity("Aluno2", new DateTime(2000, 1, 1), "22222222222", "a2@email.com", "Senha123!"));
            await context.SaveChangesAsync();

            var result = await repo.GetTotalCountAsync();
            result.Should().Be(2);
        }
    }
} 