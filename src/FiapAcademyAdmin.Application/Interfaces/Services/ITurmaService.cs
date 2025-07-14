using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Application.Interfaces.Services
{
    public interface ITurmaService
    {
        Task<IEnumerable<TurmaEntity>> GetAllAsync();
        Task<IEnumerable<TurmaEntity>> GetAllAsync(int page, int pageSize, string? filtro = null);
        Task<TurmaEntity?> GetByIdAsync(int id);
        Task<TurmaEntity> CreateAsync(TurmaEntity turma);
        Task<TurmaEntity?> UpdateAsync(TurmaEntity turma);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync(string? filtro = null);
    }
} 