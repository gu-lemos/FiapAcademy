using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Infrastructure.Services
{
    public class MatriculaService(IMatriculaRepository matriculaRepository) : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository = matriculaRepository;

        public async Task<IEnumerable<MatriculaEntity>> GetAllAsync()
        {
            return await _matriculaRepository.GetAllAsync();
        }

        public async Task<MatriculaEntity?> GetByIdAsync(int id)
        {
            return await _matriculaRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MatriculaEntity>> GetByAlunoIdAsync(int alunoId)
        {
            return await _matriculaRepository.GetByAlunoIdAsync(alunoId);
        }

        public async Task<IEnumerable<MatriculaEntity>> GetByTurmaIdAsync(int turmaId)
        {
            return await _matriculaRepository.GetByTurmaIdAsync(turmaId);
        }

        public async Task<MatriculaEntity?> GetByAlunoAndTurmaAsync(int alunoId, int turmaId)
        {
            return await _matriculaRepository.GetByAlunoAndTurmaAsync(alunoId, turmaId);
        }

        public async Task<bool> ExistsByAlunoAndTurmaAsync(int alunoId, int turmaId)
        {
            return await _matriculaRepository.ExistsByAlunoAndTurmaAsync(alunoId, turmaId);
        }

        public async Task<MatriculaEntity> CreateAsync(MatriculaEntity matricula)
        {
            if (!MatriculaEntity.MatriculaEhValida(matricula.AlunoId, matricula.TurmaId))
            {
                throw new InvalidOperationException("Dados da matrícula são inválidos.");
            }

            var exists = await _matriculaRepository.ExistsByAlunoAndTurmaAsync(matricula.AlunoId, matricula.TurmaId);
            if (exists)
            {
                throw new InvalidOperationException("Aluno já está matriculado nesta turma.");
            }

            return await _matriculaRepository.AddAsync(matricula);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _matriculaRepository.DeleteAsync(id);
        }

        public async Task<bool> DeleteByAlunoAndTurmaAsync(int alunoId, int turmaId)
        {
            return await _matriculaRepository.DeleteByAlunoAndTurmaAsync(alunoId, turmaId);
        }

        public async Task<int> GetCountByTurmaIdAsync(int turmaId)
        {
            return await _matriculaRepository.GetCountByTurmaIdAsync(turmaId);
        }
    }
} 