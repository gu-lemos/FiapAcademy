using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAlunosDisponiveis
{
    public sealed class GetAlunosDisponiveisQuery : IRequest<ResultViewModel<List<AlunoQueryDTO>>>
    {
        public int TurmaId { get; set; }
    }
} 