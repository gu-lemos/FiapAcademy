using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetById
{
    public sealed class GetTurmaByIdQuery : IRequest<ResultViewModel<TurmaQueryDTO>>
    {
        public int Id { get; set; }
    }
} 