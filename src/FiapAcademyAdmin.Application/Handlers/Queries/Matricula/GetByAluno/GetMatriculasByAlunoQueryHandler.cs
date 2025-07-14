using AutoMapper;
using FiapAcademyAdmin.Application.DTOs.Query.Matricula;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Matricula.GetByAluno
{
    public class GetMatriculasByAlunoQueryHandler(IMatriculaService matriculaService, IMapper mapper) : IRequestHandler<GetMatriculasByAlunoQuery, ResultViewModel<List<MatriculaQueryDTO>>>
    {
        private readonly IMatriculaService _matriculaService = matriculaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<List<MatriculaQueryDTO>>> Handle(GetMatriculasByAlunoQuery request, CancellationToken cancellationToken)
        {
            var matriculas = await _matriculaService.GetByAlunoIdAsync(request.AlunoId);
            var matriculasDTO = _mapper.Map<List<MatriculaQueryDTO>>(matriculas);

            return ResultViewModel<List<MatriculaQueryDTO>>.Success(matriculasDTO);
        }
    }
} 