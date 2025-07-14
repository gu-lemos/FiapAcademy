using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using MediatR;
using AutoMapper;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAlunos
{
    public class GetTurmaAlunosQueryHandler(ITurmaService turmaService, IMapper mapper) : IRequestHandler<GetTurmaAlunosQuery, ResultViewModel<TurmaAlunosQueryDTO>>
    {
        private readonly ITurmaService _turmaService = turmaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<TurmaAlunosQueryDTO>> Handle(GetTurmaAlunosQuery request, CancellationToken cancellationToken)
        {
            var turma = await _turmaService.GetByIdAsync(request.TurmaId);
            if (turma == null)
            {
                return ResultViewModel<TurmaAlunosQueryDTO>.Error("Turma não encontrada");
            }

            var alunosMatriculados = turma.GetAlunosMatriculados();
            var alunosDTO = _mapper.Map<List<AlunoQueryDTO>>(alunosMatriculados);

            var turmaDTO = new TurmaAlunosQueryDTO
            {
                TurmaId = turma.Id,
                TurmaNome = turma.Nome,
                Alunos = alunosDTO
            };

            return ResultViewModel<TurmaAlunosQueryDTO>.Success(turmaDTO);
        }
    }
} 