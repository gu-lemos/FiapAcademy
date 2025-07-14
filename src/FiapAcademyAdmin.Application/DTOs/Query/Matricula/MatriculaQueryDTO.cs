namespace FiapAcademyAdmin.Application.DTOs.Query.Matricula
{
    public sealed class MatriculaQueryDTO
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataMatricula { get; set; }
        
        public string AlunoNome { get; set; } = string.Empty;
        public string TurmaNome { get; set; } = string.Empty;
    }
} 