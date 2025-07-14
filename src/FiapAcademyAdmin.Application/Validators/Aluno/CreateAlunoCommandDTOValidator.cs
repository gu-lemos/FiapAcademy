using FluentValidation;
using FiapAcademyAdmin.Application.DTOs.Command.Aluno;

namespace FiapAcademyAdmin.Application.Validators.Aluno
{
    public class CreateAlunoCommandDTOValidator : AbstractValidator<CreateAlunoCommandDTO>
    {
        public CreateAlunoCommandDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória")
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior a hoje")
                .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Data de nascimento inválida");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Must(ValidationHelpers.BeValidCpf).WithMessage("CPF deve conter 11 dígitos numéricos");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .EmailAddress().WithMessage("E-mail deve conter o caractere @ e ser válido")
                .MaximumLength(100).WithMessage("E-mail deve ter no máximo 100 caracteres");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória")
                .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres")
                .Must(ValidationHelpers.BeStrongPassword).WithMessage("Senha deve conter letras maiúsculas, minúsculas, números e símbolos especiais");
        }
    }
} 