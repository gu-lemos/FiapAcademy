using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using MediatR;
using AutoMapper;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAll
{
    public class GetTurmasQueryHandler(ITurmaService turmaService, IMapper mapper) : IRequestHandler<GetTurmasQuery, ResultViewModel<TurmaListQueryDTO>>
    {
        private readonly ITurmaService _turmaService = turmaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<TurmaListQueryDTO>> Handle(GetTurmasQuery request, CancellationToken cancellationToken)
        {
            var turmas = await _turmaService.GetAllAsync(request.Page, request.PageSize, request.Filtro);
            var totalDeRegistros = await _turmaService.GetTotalCountAsync(request.Filtro);

            var turmasDTO = _mapper.Map<List<TurmaQueryDTO>>(turmas);
            var totalDePaginas = (int)Math.Ceiling((double)totalDeRegistros / request.PageSize);

            var result = new TurmaListQueryDTO
            {
                Turmas = turmasDTO,
                TotalCount = totalDeRegistros,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = totalDePaginas
            };

            return ResultViewModel<TurmaListQueryDTO>.Success(result);
        }
    }
} 