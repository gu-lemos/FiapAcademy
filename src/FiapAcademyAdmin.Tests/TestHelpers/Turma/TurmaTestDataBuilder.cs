using Bogus;
using FiapAcademyAdmin.Application.DTOs.Command.Turma;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Tests.TestHelpers.Turma
{
    public static class TurmaTestDataBuilder
    {
        public static CreateTurmaCommandDTO CreateTurmaCommandDTO()
        {
            return new Faker<CreateTurmaCommandDTO>()
                .RuleFor(t => t.Nome, f => f.Commerce.ProductName())
                .RuleFor(t => t.Descricao, f => f.Lorem.Sentence(10, 20))
                .RuleFor(t => t.AlunoIds, f => new List<int>())
                .Generate();
        }

        public static UpdateTurmaCommandDTO UpdateTurmaCommandDTO(int id = 1)
        {
            return new Faker<UpdateTurmaCommandDTO>()
                .RuleFor(t => t.Id, id)
                .RuleFor(t => t.Nome, f => f.Commerce.ProductName())
                .RuleFor(t => t.Descricao, f => f.Lorem.Sentence(10, 20))
                .RuleFor(t => t.AlunoIds, f => new List<int>())
                .Generate();
        }

        public static TurmaQueryDTO TurmaQueryDTO()
        {
            return new Faker<TurmaQueryDTO>()
                .RuleFor(t => t.Id, f => f.Random.Int(1, 1000))
                .RuleFor(t => t.Nome, f => f.Commerce.ProductName())
                .RuleFor(t => t.Descricao, f => f.Lorem.Sentence(10, 20))
                .RuleFor(t => t.DataCadastro, f => f.Date.Past(30))
                .RuleFor(t => t.QuantidadeAlunos, f => f.Random.Int(0, 30))
                .RuleFor(t => t.Alunos, f => new List<FiapAcademyAdmin.Application.DTOs.Query.Aluno.AlunoQueryDTO>())
                .Generate();
        }

        public static TurmaEntity TurmaEntity()
        {
            return new Faker<TurmaEntity>()
                .CustomInstantiator(f => new TurmaEntity(
                    f.Commerce.ProductName(),
                    f.Lorem.Sentence()
                ))
                .Generate();
        }

        public static List<TurmaEntity> TurmaEntities(int count = 5)
        {
            return new Faker<TurmaEntity>()
                .CustomInstantiator(f => new TurmaEntity(
                    f.Commerce.ProductName(),
                    f.Lorem.Sentence()
                ))
                .Generate(count);
        }

        public static List<TurmaQueryDTO> TurmaQueryDTOs(int count = 5)
        {
            return new Faker<TurmaQueryDTO>()
                .RuleFor(t => t.Id, f => f.Random.Int(1, 1000))
                .RuleFor(t => t.Nome, f => f.Commerce.ProductName())
                .RuleFor(t => t.Descricao, f => f.Lorem.Sentence(10, 20))
                .RuleFor(t => t.DataCadastro, f => f.Date.Past(30))
                .RuleFor(t => t.QuantidadeAlunos, f => f.Random.Int(0, 30))
                .RuleFor(t => t.Alunos, f => new List<FiapAcademyAdmin.Application.DTOs.Query.Aluno.AlunoQueryDTO>())
                .Generate(count);
        }
    }
} 