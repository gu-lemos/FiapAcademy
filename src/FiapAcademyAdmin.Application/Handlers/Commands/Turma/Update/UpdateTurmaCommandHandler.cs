using AutoMapper;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Domain.Entities;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Turma.Update
{
    public class UpdateTurmaCommandHandler(ITurmaService turmaService, IMatriculaService matriculaService, IMapper mapper) : IRequestHandler<UpdateTurmaCommand, ResultViewModel<TurmaQueryDTO>>
    {
        private readonly ITurmaService _turmaService = turmaService;
        private readonly IMatriculaService _matriculaService = matriculaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<TurmaQueryDTO>> Handle(UpdateTurmaCommand request, CancellationToken cancellationToken)
        {
            var turmaExistente = await _turmaService.GetByIdAsync(request.Turma.Id);
            if (turmaExistente == null)
            {
                return ResultViewModel<TurmaQueryDTO>.Error("Turma não encontrada");
            }

            var matriculasExistentes = await _matriculaService.GetByTurmaIdAsync(request.Turma.Id);
            foreach (var matricula in matriculasExistentes)
            {
                await _matriculaService.DeleteByAlunoAndTurmaAsync(matricula.AlunoId, matricula.TurmaId);
            }

            turmaExistente.Atualizar(request.Turma.Nome, request.Turma.Descricao);

            if (request.Turma.AlunoIds.Count != 0)
            {
                foreach (var alunoId in request.Turma.AlunoIds)
                {
                    var matricula = new MatriculaEntity(alunoId, turmaExistente.Id);
                    await _matriculaService.CreateAsync(matricula);
                }
            }

            var turmaAtualizada = await _turmaService.UpdateAsync(turmaExistente);
            var turmaDTO = _mapper.Map<TurmaQueryDTO>(turmaAtualizada);

            return ResultViewModel<TurmaQueryDTO>.Success(turmaDTO, "Turma atualizada com sucesso");
        }
    }
} 