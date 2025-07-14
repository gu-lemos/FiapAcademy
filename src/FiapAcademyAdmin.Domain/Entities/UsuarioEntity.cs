namespace FiapAcademyAdmin.Domain.Entities
{
    public sealed class UsuarioEntity(string nome, string email, string senha)
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = nome;
        public string Email { get; private set; } = email;
        public string Senha { get; private set; } = senha;
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; private set; }
        public bool Ativo { get; private set; } = true;
        public string Role => "Admin";
    }
} 