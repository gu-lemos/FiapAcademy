using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.DTOs.Query.Turma
{
    public sealed class TurmaAlunosQueryDTO
    {
        public int TurmaId { get; set; }
        public string TurmaNome { get; set; } = string.Empty;
        public List<AlunoQueryDTO> Alunos { get; set; } = [];
    }
} 