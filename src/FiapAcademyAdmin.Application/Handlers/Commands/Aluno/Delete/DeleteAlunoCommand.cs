using MediatR;
using FiapAcademyAdmin.Application.Models;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Delete
{
    public sealed class DeleteAlunoCommand : IRequest<ResultViewModel<bool>>
    {
        public int Id { get; set; }
    }
} 