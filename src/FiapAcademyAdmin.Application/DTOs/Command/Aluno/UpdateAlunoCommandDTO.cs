namespace FiapAcademyAdmin.Application.DTOs.Command.Aluno
{
    public sealed class UpdateAlunoCommandDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Senha { get; set; }
        public List<int> TurmaIds { get; set; } = [];
    }
} 