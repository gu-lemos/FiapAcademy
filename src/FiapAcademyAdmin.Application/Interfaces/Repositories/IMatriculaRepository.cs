using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Application.Interfaces.Repositories
{
    public interface IMatriculaRepository
    {
        Task<IEnumerable<MatriculaEntity>> GetAllAsync();
        Task<MatriculaEntity?> GetByIdAsync(int id);
        Task<IEnumerable<MatriculaEntity>> GetByAlunoIdAsync(int alunoId);
        Task<IEnumerable<MatriculaEntity>> GetByTurmaIdAsync(int turmaId);
        Task<MatriculaEntity?> GetByAlunoAndTurmaAsync(int alunoId, int turmaId);
        Task<bool> ExistsByAlunoAndTurmaAsync(int alunoId, int turmaId);
        Task<MatriculaEntity> AddAsync(MatriculaEntity matricula);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByAlunoAndTurmaAsync(int alunoId, int turmaId);
        Task<int> GetCountByTurmaIdAsync(int turmaId);
    }
} 