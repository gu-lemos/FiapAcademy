using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<UsuarioEntity?> GetByIdAsync(int id);
        Task<UsuarioEntity?> GetByEmailAsync(string email);
        Task<UsuarioEntity> AddAsync(UsuarioEntity usuario);
        Task<UsuarioEntity> UpdateAsync(UsuarioEntity usuario);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<IEnumerable<UsuarioEntity>> GetAllAsync();
    }
} 