using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Auth.Register
{
    public sealed class RegisterCommand(RegisterCommandDTO registerDto) : IRequest<bool>
    {
        public RegisterCommandDTO RegisterDto { get; } = registerDto;
    }
} 