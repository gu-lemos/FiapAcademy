using AutoMapper;
using MediatR;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Update
{
    public class UpdateAlunoCommandHandler(IAlunoService alunoService, ITurmaService turmaService, IMatriculaService matriculaService, IMapper mapper) : IRequestHandler<UpdateAlunoCommand, ResultViewModel<AlunoQueryDTO>>
    {
        private readonly IAlunoService _alunoService = alunoService;
        private readonly ITurmaService _turmaService = turmaService;
        private readonly IMatriculaService _matriculaService = matriculaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<AlunoQueryDTO>> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
        {
            var alunoAtual = await _alunoService.GetByIdAsync(request.Aluno.Id);

            if (alunoAtual == null)
                return ResultViewModel<AlunoQueryDTO>.Error("Aluno não encontrado");

            if (request.Aluno.Cpf != alunoAtual.Cpf)
            {
                var cpfJaExiste = await _alunoService.ExistsByCpfAsync(request.Aluno.Cpf);
                if (cpfJaExiste)
                {
                    return ResultViewModel<AlunoQueryDTO>.Error("Já existe um aluno cadastrado com este CPF.");
                }
            }

            if (request.Aluno.Email != alunoAtual.Email)
            {
                var emailJaExiste = await _alunoService.ExistsByEmailAsync(request.Aluno.Email);
                if (emailJaExiste)
                {
                    return ResultViewModel<AlunoQueryDTO>.Error("Já existe um aluno cadastrado com este e-mail.");
                }
            }

            var alunoParaUpdate = new AlunoEntity(
                request.Aluno.Nome,
                request.Aluno.DataNascimento,
                request.Aluno.Cpf,
                request.Aluno.Email,
                string.IsNullOrWhiteSpace(request.Aluno.Senha) ? alunoAtual.Senha : request.Aluno.Senha
            );

            alunoParaUpdate.DefinirId(request.Aluno.Id);

            var matriculasExistentes = await _matriculaService.GetByAlunoIdAsync(request.Aluno.Id);
            
            foreach (var matricula in matriculasExistentes)
            {
                await _matriculaService.DeleteByAlunoAndTurmaAsync(matricula.AlunoId, matricula.TurmaId);
            }

            if (request.Aluno.TurmaIds.Count > 0)
            {
                foreach (var turmaId in request.Aluno.TurmaIds)
                {
                    var turma = await _turmaService.GetByIdAsync(turmaId);

                    if (turma == null)
                        continue;

                    var novaMatricula = new MatriculaEntity(alunoParaUpdate.Id, turma.Id);
                    await _matriculaService.CreateAsync(novaMatricula);
                }
            }

            var alunoAtualizado = await _alunoService.UpdateAsync(alunoParaUpdate);
            var result = _mapper.Map<AlunoQueryDTO>(alunoAtualizado);

            return ResultViewModel<AlunoQueryDTO>.Success(result);
        }
    }
} 