using FiapAcademyAdmin.Application.DTOs.Command.Turma;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Turma.Update
{
    public sealed record UpdateTurmaCommand : IRequest<ResultViewModel<TurmaQueryDTO>>
    {
        public UpdateTurmaCommandDTO Turma { get; set; } = new();
    }
} 