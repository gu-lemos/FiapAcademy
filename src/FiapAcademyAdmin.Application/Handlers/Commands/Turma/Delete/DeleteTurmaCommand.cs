using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Turma.Delete
{
    public sealed class DeleteTurmaCommand : IRequest<ResultViewModel<bool>>
    {
        public int Id { get; set; }
    }
} 