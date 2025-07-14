using FiapAcademyAdmin.Application.Interfaces.Services;
using MediatR;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Auth.Register
{
    public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, bool>
    {
        private readonly IAuthService _authService = authService;

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request.RegisterDto);
        }
    }
} 