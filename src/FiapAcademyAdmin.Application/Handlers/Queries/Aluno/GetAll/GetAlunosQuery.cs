using MediatR;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetAll
{
    public sealed class GetAlunosQuery : IRequest<ResultViewModel<AlunoListQueryDTO>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Filtro { get; set; }
    }
} 