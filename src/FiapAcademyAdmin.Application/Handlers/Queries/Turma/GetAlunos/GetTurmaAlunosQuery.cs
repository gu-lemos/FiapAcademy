using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAlunos
{
    public sealed class GetTurmaAlunosQuery : IRequest<ResultViewModel<TurmaAlunosQueryDTO>>
    {
        public int TurmaId { get; set; }
    }
} 