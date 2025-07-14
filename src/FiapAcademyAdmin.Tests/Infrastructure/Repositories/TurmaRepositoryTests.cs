using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;
using FiapAcademyAdmin.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FiapAcademyAdmin.Tests.Infrastructure.Repositories
{
    public class TurmaRepositoryTests
    {
        private static ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_DeveAdicionarTurma()
        {
            using var context = CreateInMemoryContext();
            var repo = new TurmaRepository(context);
            var turma = new TurmaEntity("Turma Teste", "Descrição Teste");

            var result = await repo.CreateAsync(turma);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            (await context.Turmas.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarTurmaExistente()
        {
            using var context = CreateInMemoryContext();
            var repo = new TurmaRepository(context);
            var turma = new TurmaEntity("Turma Teste", "Descrição Teste");
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();

            var result = await repo.GetByIdAsync(turma.Id);

            result.Should().NotBeNull();
            result!.Nome.Should().Be("Turma Teste");
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodasTurmas()
        {
            using var context = CreateInMemoryContext();
            var repo = new TurmaRepository(context);
            context.Turmas.Add(new TurmaEntity("Turma1", "Desc1"));
            context.Turmas.Add(new TurmaEntity("Turma2", "Desc2"));
            await context.SaveChangesAsync();

            var result = await repo.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarTurma()
        {
            using var context = CreateInMemoryContext();
            var repo = new TurmaRepository(context);
            var turma = new TurmaEntity("Turma Teste", "Descrição Teste");
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();

            turma.Atualizar("Nova Turma", "Nova Descrição");
            var result = await repo.UpdateAsync(turma);

            result.Should().NotBeNull();
            result!.Nome.Should().Be("Nova Turma");
            result.Descricao.Should().Be("Nova Descrição");
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverTurma()
        {
            using var context = CreateInMemoryContext();
            var repo = new TurmaRepository(context);
            var turma = new TurmaEntity("Turma Teste", "Descrição Teste");
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();

            var result = await repo.DeleteAsync(turma.Id);

            result.Should().BeTrue();
            (await context.Turmas.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task GetTotalCountAsync_DeveRetornarQuantidadeCorreta()
        {
            using var context = CreateInMemoryContext();
            var repo = new TurmaRepository(context);
            context.Turmas.Add(new TurmaEntity("Turma1", "Desc1"));
            context.Turmas.Add(new TurmaEntity("Turma2", "Desc2"));
            await context.SaveChangesAsync();

            var result = await repo.GetTotalCountAsync();
            result.Should().Be(2);
        }
    }
} 