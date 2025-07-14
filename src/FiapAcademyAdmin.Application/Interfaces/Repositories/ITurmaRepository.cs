using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Application.Interfaces.Repositories
{
    public interface ITurmaRepository
    {
        Task<IEnumerable<TurmaEntity>> GetAllAsync();
        Task<IEnumerable<TurmaEntity>> GetAllAsync(int page, int pageSize, string? filtro = null);
        Task<TurmaEntity?> GetByIdAsync(int id);
        Task<TurmaEntity> CreateAsync(TurmaEntity turma);
        Task<TurmaEntity?> UpdateAsync(TurmaEntity turma);
        Task<bool> DeleteAsync(int id);
        Task<int> GetTotalCountAsync(string? filtro = null);
    }
} 