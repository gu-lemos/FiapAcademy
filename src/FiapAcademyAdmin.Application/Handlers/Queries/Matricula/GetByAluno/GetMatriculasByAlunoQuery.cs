using FiapAcademyAdmin.Application.DTOs.Query.Matricula;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Matricula.GetByAluno
{
    public sealed class GetMatriculasByAlunoQuery : IRequest<ResultViewModel<List<MatriculaQueryDTO>>>
    {
        public int AlunoId { get; set; }
    }
} 