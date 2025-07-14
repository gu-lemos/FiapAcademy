using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace FiapAcademyAdmin.Infrastructure.Services
{
    public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;

        public async Task<IEnumerable<AlunoEntity>> GetAllAsync(int page = 1, int pageSize = 10, string? filtro = null)
        {
            return await _alunoRepository.GetAllAsync(page, pageSize, filtro);
        }

        public async Task<AlunoEntity?> GetByIdAsync(int id)
        {
            return await _alunoRepository.GetByIdAsync(id);
        }

        public async Task<AlunoEntity> CreateAsync(AlunoEntity aluno)
        {
            var novoAluno = new AlunoEntity(
                aluno.Nome,
                aluno.DataNascimento,
                aluno.Cpf,
                aluno.Email,
                HashPassword(aluno.Senha)
            );
            
            return await _alunoRepository.AddAsync(novoAluno);
        }

        public async Task<AlunoEntity?> UpdateAsync(AlunoEntity aluno)
        {
            if (!string.IsNullOrWhiteSpace(aluno.Senha))
            {
                aluno.AtualizarSenha(HashPassword(aluno.Senha));
            }
            
            return await _alunoRepository.UpdateAsync(aluno);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _alunoRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalCountAsync(string? filtro = null)
        {
            return await _alunoRepository.GetTotalCountAsync(filtro);
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _alunoRepository.ExistsByCpfAsync(cpf);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _alunoRepository.ExistsByEmailAsync(email);
        }

        private static string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 