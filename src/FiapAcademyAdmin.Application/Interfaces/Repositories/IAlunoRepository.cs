using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Application.Interfaces.Repositories
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<AlunoEntity>> GetAllAsync(int page = 1, int pageSize = 10, string? filtro = null);
        Task<AlunoEntity?> GetByIdAsync(int id);
        Task<AlunoEntity> AddAsync(AlunoEntity aluno);
        Task<AlunoEntity?> UpdateAsync(AlunoEntity aluno);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<bool> ExistsByEmailAsync(string email);
        Task<int> GetTotalCountAsync(string? filtro = null);
    }
} 