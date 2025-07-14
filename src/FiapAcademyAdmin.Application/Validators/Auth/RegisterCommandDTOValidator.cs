using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FluentValidation;

namespace FiapAcademyAdmin.Application.Validators.Auth
{
    public class RegisterCommandDTOValidator : AbstractValidator<RegisterCommandDTO>
    {
        public RegisterCommandDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Must(ValidationHelpers.BeStrongPassword).WithMessage("A senha deve conter letras maiúsculas, minúsculas, números e símbolos especiais.");

            RuleFor(x => x.ConfirmarSenha)
                .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
                .Equal(x => x.Senha).WithMessage("As senhas não coincidem.");
        }
    }
} 