using FluentValidation;
using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.Validators;

namespace FiapAcademyAdmin.Application.Validators.Auth
{
    public class LoginCommandDTOValidator : AbstractValidator<LoginCommandDTO>
    {
        public LoginCommandDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
} 