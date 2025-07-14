using MediatR;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetById
{
    public sealed class GetAlunoByIdQuery : IRequest<ResultViewModel<AlunoQueryDTO>>
    {
        public int Id { get; set; }
    }
} 