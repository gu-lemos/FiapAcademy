using AutoMapper;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Domain.Entities;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Turma.Create
{
    public class CreateTurmaCommandHandler(ITurmaService turmaService, IMatriculaService matriculaService, IMapper mapper) : IRequestHandler<CreateTurmaCommand, ResultViewModel<TurmaQueryDTO>>
    {
        private readonly ITurmaService _turmaService = turmaService;
        private readonly IMatriculaService _matriculaService = matriculaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<TurmaQueryDTO>> Handle(CreateTurmaCommand request, CancellationToken cancellationToken)
        {
            var turma = new TurmaEntity(request.Turma.Nome, request.Turma.Descricao);
            
            var turmaCriada = await _turmaService.CreateAsync(turma);
            
            if (request.Turma.AlunoIds.Count != 0)
            {
                foreach (var alunoId in request.Turma.AlunoIds)
                {
                    var matricula = new MatriculaEntity(alunoId, turmaCriada.Id);
                    await _matriculaService.CreateAsync(matricula);
                }
            }

            var turmaDTO = _mapper.Map<TurmaQueryDTO>(turmaCriada);

            return ResultViewModel<TurmaQueryDTO>.Success(turmaDTO, "Turma criada com sucesso");
        }
    }
} 