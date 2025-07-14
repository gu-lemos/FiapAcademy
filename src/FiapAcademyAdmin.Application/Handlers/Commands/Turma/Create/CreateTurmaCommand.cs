using FiapAcademyAdmin.Application.DTOs.Command.Turma;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Turma.Create
{
    public sealed class CreateTurmaCommand : IRequest<ResultViewModel<TurmaQueryDTO>>
    {
        public CreateTurmaCommandDTO Turma { get; set; } = new();
    }
} 