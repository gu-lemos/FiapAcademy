using Bogus;
using FiapAcademyAdmin.Application.DTOs.Command.Aluno;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Tests.TestHelpers.Aluno
{
    public static class AlunoTestDataBuilder
    {
        private static string GenerateValidPassword()
        {
            return "Abc123!@";
        }

        public static CreateAlunoCommandDTO CreateAlunoCommandDTO()
        {
            return new Faker<CreateAlunoCommandDTO>()
                .RuleFor(a => a.Nome, f => f.Name.FullName())
                .RuleFor(a => a.DataNascimento, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(a => a.Cpf, f => f.Random.ReplaceNumbers("###########"))
                .RuleFor(a => a.Email, f => f.Internet.Email())
                .RuleFor(a => a.Senha, f => GenerateValidPassword())
                .RuleFor(a => a.TurmaIds, f => new List<int>())
                .Generate();
        }

        public static UpdateAlunoCommandDTO UpdateAlunoCommandDTO(int id = 1)
        {
            return new Faker<UpdateAlunoCommandDTO>()
                .RuleFor(a => a.Id, id)
                .RuleFor(a => a.Nome, f => f.Name.FullName())
                .RuleFor(a => a.DataNascimento, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(a => a.Cpf, f => f.Random.ReplaceNumbers("###########"))
                .RuleFor(a => a.Email, f => f.Internet.Email())
                .RuleFor(a => a.Senha, f => GenerateValidPassword())
                .RuleFor(a => a.TurmaIds, f => new List<int>())
                .Generate();
        }

        public static AlunoQueryDTO AlunoQueryDTO()
        {
            return new Faker<AlunoQueryDTO>()
                .RuleFor(a => a.Id, f => f.Random.Int(1, 1000))
                .RuleFor(a => a.Nome, f => f.Name.FullName())
                .RuleFor(a => a.DataNascimento, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(a => a.Cpf, f => f.Random.ReplaceNumbers("###########"))
                .RuleFor(a => a.Email, f => f.Internet.Email())
                .RuleFor(a => a.DataCadastro, f => f.Date.Past(30))
                .RuleFor(a => a.DataAtualizacao, (System.DateTime?)null)
                .RuleFor(a => a.Turmas, f => new List<string>())
                .Generate();
        }

        public static AlunoEntity AlunoEntity()
        {
            return new Faker<AlunoEntity>()
                .CustomInstantiator(f => new AlunoEntity(
                    f.Name.FullName(),
                    f.Date.Past(20, System.DateTime.Now.AddYears(-18)),
                    f.Random.ReplaceNumbers("###########"),
                    f.Internet.Email(),
                    GenerateValidPassword()
                ))
                .Generate();
        }

        public static List<AlunoEntity> AlunoEntities(int count = 5)
        {
            return new Faker<AlunoEntity>()
                .CustomInstantiator(f => new AlunoEntity(
                    f.Name.FullName(),
                    f.Date.Past(20, System.DateTime.Now.AddYears(-18)),
                    f.Random.ReplaceNumbers("###########"),
                    f.Internet.Email(),
                    GenerateValidPassword()
                ))
                .Generate(count);
        }

        public static List<AlunoQueryDTO> AlunoQueryDTOs(int count = 5)
        {
            return new Faker<AlunoQueryDTO>()
                .RuleFor(a => a.Id, f => f.Random.Int(1, 1000))
                .RuleFor(a => a.Nome, f => f.Name.FullName())
                .RuleFor(a => a.DataNascimento, f => f.Date.Past(20, System.DateTime.Now.AddYears(-18)))
                .RuleFor(a => a.Cpf, f => f.Random.ReplaceNumbers("###########"))
                .RuleFor(a => a.Email, f => f.Internet.Email())
                .RuleFor(a => a.DataCadastro, f => f.Date.Past(30))
                .RuleFor(a => a.DataAtualizacao, (System.DateTime?)null)
                .RuleFor(a => a.Turmas, f => new List<string>())
                .Generate(count);
        }
    }
} 