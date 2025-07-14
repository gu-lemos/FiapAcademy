using MediatR;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Application.DTOs.Command.Aluno;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Update
{
    public sealed class UpdateAlunoCommand : IRequest<ResultViewModel<AlunoQueryDTO>>
    {
        public UpdateAlunoCommandDTO Aluno { get; set; } = new();
    }
} 