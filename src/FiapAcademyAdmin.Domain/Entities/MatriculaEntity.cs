namespace FiapAcademyAdmin.Domain.Entities
{
    public sealed class MatriculaEntity(int alunoId, int turmaId)
    {
        public int Id { get; private set; }
        public int AlunoId { get; private set; } = alunoId;
        public int TurmaId { get; private set; } = turmaId;
        public DateTime DataMatricula { get; private set; } = DateTime.Now;

        public AlunoEntity Aluno { get; private set; } = null!;
        public TurmaEntity Turma { get; private set; } = null!;

        public static bool MatriculaEhValida(int alunoId, int turmaId)
        {
            return alunoId > 0 && turmaId > 0;
        }
    }
} 