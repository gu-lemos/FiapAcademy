using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using MediatR;
using AutoMapper;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetById
{
    public class GetTurmaByIdQueryHandler(ITurmaService turmaService, IMapper mapper) : IRequestHandler<GetTurmaByIdQuery, ResultViewModel<TurmaQueryDTO>>
    {
        private readonly ITurmaService _turmaService = turmaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<TurmaQueryDTO>> Handle(GetTurmaByIdQuery request, CancellationToken cancellationToken)
        {
            var turma = await _turmaService.GetByIdAsync(request.Id);
            if (turma == null)
            {
                return ResultViewModel<TurmaQueryDTO>.Error("Turma não encontrada");
            }

            var turmaDTO = _mapper.Map<TurmaQueryDTO>(turma);
            return ResultViewModel<TurmaQueryDTO>.Success(turmaDTO);
        }
    }
} 