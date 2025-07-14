using Microsoft.EntityFrameworkCore;
using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;

namespace FiapAcademyAdmin.Infrastructure.Repositories
{
    public class AlunoRepository(ApplicationDbContext context) : IAlunoRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<AlunoEntity>> GetAllAsync(int page = 1, int pageSize = 10, string? filtro = null)
        {
            var query = _context.Alunos
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.Turma)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(aluno =>
                    aluno.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    aluno.Cpf.Contains(filtro) ||
                    aluno.Email.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                );
            }

            return await query
                .OrderBy(aluno => aluno.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<AlunoEntity?> GetByIdAsync(int id)
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                    .ThenInclude(m => m.Turma)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AlunoEntity> AddAsync(AlunoEntity aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<AlunoEntity?> UpdateAsync(AlunoEntity aluno)
        {
            var alunoParaEditar = await _context.Alunos.FindAsync(aluno.Id);

            if (alunoParaEditar is not null)
            {
                alunoParaEditar.Atualizar(
                    aluno.Nome,
                    aluno.DataNascimento,
                    aluno.Cpf,
                    aluno.Email,
                    aluno.Senha
                );

                await _context.SaveChangesAsync();
            }

            return alunoParaEditar;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aluno = await GetByIdAsync(id);

            if (aluno == null)
                return false;

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetTotalCountAsync(string? filtro = null)
        {
            var query = _context.Alunos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(aluno =>
                    aluno.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                    aluno.Cpf.Contains(filtro) ||
                    aluno.Email.Contains(filtro, StringComparison.OrdinalIgnoreCase)
                );
            }

            return await query.CountAsync();
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.Alunos.AnyAsync(aluno => aluno.Cpf == cpf);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Alunos.AnyAsync(aluno => aluno.Email == email);
        }
    }
} 