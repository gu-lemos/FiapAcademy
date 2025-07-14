using FiapAcademyAdmin.Application.DTOs.Command.Aluno;
using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.DTOs.Query.Auth;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Auth.Login
{
    public sealed class LoginCommand(LoginCommandDTO loginDto) : IRequest<LoginResponseDTO>
    {
        public LoginCommandDTO LoginDto { get; } = loginDto;
    }
} 