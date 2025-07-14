using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAll
{
    public sealed class GetTurmasQuery : IRequest<ResultViewModel<TurmaListQueryDTO>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Filtro { get; set; }
    }
} 