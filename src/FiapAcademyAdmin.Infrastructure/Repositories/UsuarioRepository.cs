using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapAcademyAdmin.Infrastructure.Repositories
{
    public class UsuarioRepository(ApplicationDbContext context) : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<UsuarioEntity?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<UsuarioEntity?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UsuarioEntity> AddAsync(UsuarioEntity usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<UsuarioEntity> UpdateAsync(UsuarioEntity usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UsuarioEntity>> GetAllAsync()
        {
            return await _context.Usuarios.OrderBy(u => u.Nome).ToListAsync();
        }
    }
} 