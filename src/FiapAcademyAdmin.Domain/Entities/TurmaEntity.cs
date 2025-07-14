namespace FiapAcademyAdmin.Domain.Entities
{
    public sealed class TurmaEntity(string nome, string descricao)
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = nome;
        public string Descricao { get; private set; } = descricao;
        public DateTime DataCadastro { get; private set; } = DateTime.UtcNow;

        public ICollection<MatriculaEntity> Matriculas { get; private set; } = new List<MatriculaEntity>();

        public void Atualizar(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public IEnumerable<AlunoEntity> GetAlunosMatriculados()
        {
            return Matriculas.Select(m => m.Aluno).Where(a => a != null);
        }

        public int GetQuantidadeAlunosMatriculados()
        {
            return Matriculas.Count;
        }
    }
} 