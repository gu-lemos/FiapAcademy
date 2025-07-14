using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.Models;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Turma.Delete
{
    public class DeleteTurmaCommandHandler(ITurmaService turmaService) : IRequestHandler<DeleteTurmaCommand, ResultViewModel<bool>>
    {
        private readonly ITurmaService _turmaService = turmaService;

        public async Task<ResultViewModel<bool>> Handle(DeleteTurmaCommand request, CancellationToken cancellationToken)
        {
            var turmaExistente = await _turmaService.GetByIdAsync(request.Id);

            if (turmaExistente == null)
            {
                return ResultViewModel<bool>.Error("Turma não encontrada");
            }

            await _turmaService.DeleteAsync(request.Id);
            return ResultViewModel<bool>.Success(true, "Turma excluída com sucesso");
        }
    }
} 