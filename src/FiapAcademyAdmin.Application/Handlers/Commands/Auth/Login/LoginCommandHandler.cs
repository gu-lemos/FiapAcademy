using FiapAcademyAdmin.Application.DTOs.Query.Auth;
using FiapAcademyAdmin.Application.Interfaces.Services;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Auth.Login
{
    public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IAuthService _authService = authService;

        public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.LoginDto);
        }
    }
} 