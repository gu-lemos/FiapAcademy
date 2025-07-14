using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Application.Interfaces.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoEntity>> GetAllAsync(int page = 1, int pageSize = 10, string? filtro = null);
        Task<AlunoEntity?> GetByIdAsync(int id);
        Task<AlunoEntity> CreateAsync(AlunoEntity aluno);
        Task<AlunoEntity?> UpdateAsync(AlunoEntity aluno);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<bool> ExistsByEmailAsync(string email);
        Task<int> GetTotalCountAsync(string? filtro = null);
    }
} 