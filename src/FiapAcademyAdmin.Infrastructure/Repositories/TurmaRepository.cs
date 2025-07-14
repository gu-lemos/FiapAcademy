using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FiapAcademyAdmin.Infrastructure.Repositories
{
    public class TurmaRepository(ApplicationDbContext context) : ITurmaRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<TurmaEntity>> GetAllAsync()
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                    .ThenInclude(m => m.Aluno)
                .OrderBy(t => t.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<TurmaEntity>> GetAllAsync(int page, int pageSize, string? filtro = null)
        {
            var query = _context.Turmas
                .Include(t => t.Matriculas)
                    .ThenInclude(m => m.Aluno)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(t => t.Nome.Contains(filtro) || t.Descricao.Contains(filtro));
            }

            return await query
                .OrderBy(t => t.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TurmaEntity?> GetByIdAsync(int id)
        {
            return await _context.Turmas
                .Include(t => t.Matriculas)
                    .ThenInclude(m => m.Aluno)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TurmaEntity> CreateAsync(TurmaEntity turma)
        {
            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
            return turma;
        }

        public async Task<TurmaEntity?> UpdateAsync(TurmaEntity turma)
        {
            var turmaParaAtualuzar = await _context.Turmas
                .Include(t => t.Matriculas)
                .FirstOrDefaultAsync(t => t.Id == turma.Id);

            if (turmaParaAtualuzar is not null)
            {
                turmaParaAtualuzar.Atualizar(turma.Nome, turma.Descricao);

                await _context.SaveChangesAsync();
            }
            return turmaParaAtualuzar;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var turma = await GetByIdAsync(id);
            if (turma == null)
                return false;

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalCountAsync(string? filtro = null)
        {
            var query = _context.Turmas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(t => t.Nome.Contains(filtro) || t.Descricao.Contains(filtro));
            }

            return await query.CountAsync();
        }
    }
} 