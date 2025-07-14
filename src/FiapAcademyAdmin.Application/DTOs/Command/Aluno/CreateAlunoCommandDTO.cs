namespace FiapAcademyAdmin.Application.DTOs.Command.Aluno
{
    public sealed class CreateAlunoCommandDTO
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public List<int> TurmaIds { get; set; } = [];
    }
} 