using MediatR;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Application.DTOs.Command.Aluno;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Create
{
    public sealed class CreateAlunoCommand : IRequest<ResultViewModel<AlunoQueryDTO>>
    {
        public CreateAlunoCommandDTO Aluno { get; set; } = new();
    }
} 