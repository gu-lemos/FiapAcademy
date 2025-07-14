using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Infrastructure.Services
{
    public class TurmaService(ITurmaRepository turmaRepository) : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository = turmaRepository;

        public async Task<IEnumerable<TurmaEntity>> GetAllAsync()
        {
            return await _turmaRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TurmaEntity>> GetAllAsync(int page, int pageSize, string? filtro = null)
        {
            return await _turmaRepository.GetAllAsync(page, pageSize, filtro);
        }

        public async Task<TurmaEntity?> GetByIdAsync(int id)
        {
            return await _turmaRepository.GetByIdAsync(id);
        }

        public async Task<TurmaEntity> CreateAsync(TurmaEntity turma)
        {
            return await _turmaRepository.CreateAsync(turma);
        }

        public async Task<TurmaEntity?> UpdateAsync(TurmaEntity turma)
        {
            return await _turmaRepository.UpdateAsync(turma);
        }

        public async Task DeleteAsync(int id)
        {
            await _turmaRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalCountAsync(string? filtro = null)
        {
            return await _turmaRepository.GetTotalCountAsync(filtro);
        }
    }
} 