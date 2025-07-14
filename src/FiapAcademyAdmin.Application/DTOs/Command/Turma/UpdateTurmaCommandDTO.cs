namespace FiapAcademyAdmin.Application.DTOs.Command.Turma
{
    public sealed class UpdateTurmaCommandDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<int> AlunoIds { get; set; } = [];
    }
} 