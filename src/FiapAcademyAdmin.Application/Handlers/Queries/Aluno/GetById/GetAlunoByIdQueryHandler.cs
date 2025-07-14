using MediatR;
using AutoMapper;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetById
{
    public class GetAlunoByIdQueryHandler(IAlunoService alunoService, IMapper mapper) : IRequestHandler<GetAlunoByIdQuery, ResultViewModel<AlunoQueryDTO>>
    {
        private readonly IAlunoService _alunoService = alunoService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<AlunoQueryDTO>> Handle(GetAlunoByIdQuery request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoService.GetByIdAsync(request.Id);
            if (aluno == null)
                return ResultViewModel<AlunoQueryDTO>.Error("Aluno não encontrado");

            var alunoDTO = _mapper.Map<AlunoQueryDTO>(aluno);
            return ResultViewModel<AlunoQueryDTO>.Success(alunoDTO);
        }
    }
} 