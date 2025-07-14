using MediatR;
using AutoMapper;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetAll
{
    public class GetAlunosQueryHandler(IAlunoService alunoService, IMapper mapper) : IRequestHandler<GetAlunosQuery, ResultViewModel<AlunoListQueryDTO>>
    {
        private readonly IAlunoService _alunoService = alunoService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<AlunoListQueryDTO>> Handle(GetAlunosQuery request, CancellationToken cancellationToken)
        {
            var alunos = await _alunoService.GetAllAsync(request.Page, request.PageSize, request.Filtro);
            var totalCount = await _alunoService.GetTotalCountAsync(request.Filtro);
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var alunosDTO = _mapper.Map<IEnumerable<AlunoQueryDTO>>(alunos);

            var result = new AlunoListQueryDTO
            {
                Alunos = alunosDTO,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = totalPages
            };

            return ResultViewModel<AlunoListQueryDTO>.Success(result);
        }
    }
} 