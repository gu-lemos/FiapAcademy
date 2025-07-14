using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;
using FiapAcademyAdmin.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FiapAcademyAdmin.Tests.Infrastructure.Repositories
{
    public class MatriculaRepositoryTests
    {
        private static ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarMatricula()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.Add(aluno);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            var matricula = new MatriculaEntity(aluno.Id, turma.Id);

            var result = await repo.AddAsync(matricula);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            (await context.Matriculas.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarMatriculaExistente()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.Add(aluno);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            var matricula = new MatriculaEntity(aluno.Id, turma.Id);
            context.Matriculas.Add(matricula);
            await context.SaveChangesAsync();

            var result = await repo.GetByIdAsync(matricula.Id);

            result.Should().NotBeNull();
            result!.AlunoId.Should().Be(aluno.Id);
            result.TurmaId.Should().Be(turma.Id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodasMatriculas()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.Add(aluno);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            context.Matriculas.Add(new MatriculaEntity(aluno.Id, turma.Id));
            context.Matriculas.Add(new MatriculaEntity(aluno.Id, turma.Id));
            await context.SaveChangesAsync();

            var result = await repo.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByAlunoIdAsync_DeveRetornarMatriculasDoAluno()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma1 = new TurmaEntity("Turma1", "Desc1");
            var turma2 = new TurmaEntity("Turma2", "Desc2");
            context.Alunos.Add(aluno);
            context.Turmas.AddRange(turma1, turma2);
            await context.SaveChangesAsync();
            context.Matriculas.Add(new MatriculaEntity(aluno.Id, turma1.Id));
            context.Matriculas.Add(new MatriculaEntity(aluno.Id, turma2.Id));
            await context.SaveChangesAsync();

            var result = await repo.GetByAlunoIdAsync(aluno.Id);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByTurmaIdAsync_DeveRetornarMatriculasDaTurma()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno1 = new AlunoEntity("Aluno1", DateTime.Today.AddYears(-20), "11111111111", "a1@email.com", "Senha123!");
            var aluno2 = new AlunoEntity("Aluno2", DateTime.Today.AddYears(-20), "22222222222", "a2@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.AddRange(aluno1, aluno2);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            context.Matriculas.Add(new MatriculaEntity(aluno1.Id, turma.Id));
            context.Matriculas.Add(new MatriculaEntity(aluno2.Id, turma.Id));
            await context.SaveChangesAsync();

            var result = await repo.GetByTurmaIdAsync(turma.Id);

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByAlunoAndTurmaAsync_DeveRetornarMatriculaCorreta()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.Add(aluno);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            var matricula = new MatriculaEntity(aluno.Id, turma.Id);
            context.Matriculas.Add(matricula);
            await context.SaveChangesAsync();

            var result = await repo.GetByAlunoAndTurmaAsync(aluno.Id, turma.Id);

            result.Should().NotBeNull();
            result!.AlunoId.Should().Be(aluno.Id);
            result.TurmaId.Should().Be(turma.Id);
        }

        [Fact]
        public async Task ExistsByAlunoAndTurmaAsync_DeveRetornarTrueSeExistir()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.Add(aluno);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            var matricula = new MatriculaEntity(aluno.Id, turma.Id);
            context.Matriculas.Add(matricula);
            await context.SaveChangesAsync();

            var result = await repo.ExistsByAlunoAndTurmaAsync(aluno.Id, turma.Id);
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverMatricula()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.Add(aluno);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            var matricula = new MatriculaEntity(aluno.Id, turma.Id);
            context.Matriculas.Add(matricula);
            await context.SaveChangesAsync();

            var result = await repo.DeleteAsync(matricula.Id);

            result.Should().BeTrue();
            (await context.Matriculas.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task DeleteByAlunoAndTurmaAsync_DeveRemoverMatriculaCorreta()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno = new AlunoEntity("Aluno Teste", DateTime.Today.AddYears(-20), "12345678900", "aluno@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.Add(aluno);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            var matricula = new MatriculaEntity(aluno.Id, turma.Id);
            context.Matriculas.Add(matricula);
            await context.SaveChangesAsync();

            var result = await repo.DeleteByAlunoAndTurmaAsync(aluno.Id, turma.Id);

            result.Should().BeTrue();
            (await context.Matriculas.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task GetCountByTurmaIdAsync_DeveRetornarQuantidadeCorreta()
        {
            using var context = CreateInMemoryContext();
            var repo = new MatriculaRepository(context);
            var aluno1 = new AlunoEntity("Aluno1", DateTime.Today.AddYears(-20), "11111111111", "a1@email.com", "Senha123!");
            var aluno2 = new AlunoEntity("Aluno2", DateTime.Today.AddYears(-20), "22222222222", "a2@email.com", "Senha123!");
            var turma = new TurmaEntity("Turma Teste", "Desc");
            context.Alunos.AddRange(aluno1, aluno2);
            context.Turmas.Add(turma);
            await context.SaveChangesAsync();
            context.Matriculas.Add(new MatriculaEntity(aluno1.Id, turma.Id));
            context.Matriculas.Add(new MatriculaEntity(aluno2.Id, turma.Id));
            await context.SaveChangesAsync();

            var result = await repo.GetCountByTurmaIdAsync(turma.Id);
            result.Should().Be(2);
        }
    }
} 