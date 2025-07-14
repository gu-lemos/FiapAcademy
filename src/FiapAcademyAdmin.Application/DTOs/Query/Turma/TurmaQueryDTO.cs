using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.DTOs.Query.Turma
{
    public sealed class TurmaQueryDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public int QuantidadeAlunos { get; set; }
        public List<AlunoQueryDTO> Alunos { get; set; } = [];
    }
} 