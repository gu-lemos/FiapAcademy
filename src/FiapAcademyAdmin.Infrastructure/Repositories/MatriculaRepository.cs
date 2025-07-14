using Microsoft.EntityFrameworkCore;
using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;

namespace FiapAcademyAdmin.Infrastructure.Repositories
{
    public class MatriculaRepository(ApplicationDbContext context) : IMatriculaRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<MatriculaEntity>> GetAllAsync()
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .OrderBy(m => m.Aluno.Nome)
                .ToListAsync();
        }

        public async Task<MatriculaEntity?> GetByIdAsync(int id)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MatriculaEntity>> GetByAlunoIdAsync(int alunoId)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .Where(m => m.AlunoId == alunoId)
                .OrderBy(m => m.Turma.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<MatriculaEntity>> GetByTurmaIdAsync(int turmaId)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .Where(m => m.TurmaId == turmaId)
                .OrderBy(m => m.Aluno.Nome)
                .ToListAsync();
        }

        public async Task<MatriculaEntity?> GetByAlunoAndTurmaAsync(int alunoId, int turmaId)
        {
            return await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.TurmaId == turmaId);
        }

        public async Task<bool> ExistsByAlunoAndTurmaAsync(int alunoId, int turmaId)
        {
            return await _context.Matriculas
                .AnyAsync(m => m.AlunoId == alunoId && m.TurmaId == turmaId);
        }

        public async Task<MatriculaEntity> AddAsync(MatriculaEntity matricula)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();
            return matricula;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var matricula = await GetByIdAsync(id);

            if (matricula == null)
                return false;

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteByAlunoAndTurmaAsync(int alunoId, int turmaId)
        {
            var matricula = await GetByAlunoAndTurmaAsync(alunoId, turmaId);

            if (matricula == null)
                return false;

            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetCountByTurmaIdAsync(int turmaId)
        {
            return await _context.Matriculas
                .CountAsync(m => m.TurmaId == turmaId);
        }
    }
} 