using MediatR;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Application.Interfaces.Services;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Delete
{
    public class DeleteAlunoCommandHandler(IAlunoService alunoService) : IRequestHandler<DeleteAlunoCommand, ResultViewModel<bool>>
    {
        private readonly IAlunoService _alunoService = alunoService;

        public async Task<ResultViewModel<bool>> Handle(DeleteAlunoCommand request, CancellationToken cancellationToken)
        {
            var result = await _alunoService.DeleteAsync(request.Id);
            return result 
                ? ResultViewModel<bool>.Success(result)
                : ResultViewModel<bool>.Error("Aluno não encontrado");
        }
    }
} 