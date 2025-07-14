namespace FiapAcademyAdmin.Domain.Entities
{
    public sealed class AlunoEntity(string nome, DateTime dataNascimento, string cpf, string email, string senha)
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = nome;
        public DateTime DataNascimento { get; private set; } = dataNascimento;
        public string Cpf { get; private set; } = cpf;
        public string Email { get; private set; } = email;
        public string Senha { get; private set; } = senha;
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; private set; }
        
        public ICollection<MatriculaEntity> Matriculas { get; private set; } = new List<MatriculaEntity>();

        public void Atualizar(string nome, DateTime dataNascimento, string cpf, string email, string senha)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Email = email;
            
            if (!string.IsNullOrWhiteSpace(senha))
            {
                Senha = senha;
            }
            
            DataAtualizacao = DateTime.Now;
        }

        public void AtualizarSenha(string novaSenha)
        {
            Senha = novaSenha;
            DataAtualizacao = DateTime.Now;
        }

        public void DefinirId(int id)
        {
            Id = id;
        }
    }
} 