using FluentValidation;
using FiapAcademyAdmin.Application.DTOs.Command.Turma;

namespace FiapAcademyAdmin.Application.Validators.Turma
{
    public class UpdateTurmaCommandDTOValidator : AbstractValidator<UpdateTurmaCommandDTO>
    {
        public UpdateTurmaCommandDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID deve ser maior que zero");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .MaximumLength(500).WithMessage("Descrição deve ter no máximo 500 caracteres")
                .MinimumLength(10).WithMessage("Descrição deve ter no mínimo 10 caracteres");
        }
    }
} 