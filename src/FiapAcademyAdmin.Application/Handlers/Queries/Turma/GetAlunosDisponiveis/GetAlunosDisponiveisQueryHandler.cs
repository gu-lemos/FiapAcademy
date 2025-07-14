using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Domain.Entities;
using MediatR;
using AutoMapper;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAlunosDisponiveis
{
    public class GetAlunosDisponiveisQueryHandler(IAlunoService alunoService, IMapper mapper) : IRequestHandler<GetAlunosDisponiveisQuery, ResultViewModel<List<AlunoQueryDTO>>>
    {
        private readonly IAlunoService _alunoService = alunoService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<List<AlunoQueryDTO>>> Handle(GetAlunosDisponiveisQuery request, CancellationToken cancellationToken)
        {
            var alunos = await _alunoService.GetAllAsync();
            
            List<AlunoEntity> alunosDisponiveis;
            
            if (request.TurmaId == 0)
            {
                alunosDisponiveis = [.. alunos];
            }
            else
            {
                alunosDisponiveis = [.. alunos.Where(a => !a.Matriculas.Any(m => m.TurmaId == request.TurmaId))];
            }

            var alunosDTO = _mapper.Map<List<AlunoQueryDTO>>(alunosDisponiveis.OrderBy(a => a.Nome).ToList());

            return ResultViewModel<List<AlunoQueryDTO>>.Success(alunosDTO);
        }
    }
} 