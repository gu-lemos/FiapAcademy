namespace FiapAcademyAdmin.Application.DTOs.Query.Aluno
{
    public sealed class AlunoQueryDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public List<string> Turmas { get; set; } = [];
    }
} 