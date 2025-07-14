using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.DTOs.Query.Auth;
using FiapAcademyAdmin.Application.Handlers.Commands.Auth.Login;
using FiapAcademyAdmin.Application.Handlers.Commands.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FiapAcademyAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginCommandDTO loginDto)
        {
            var command = new LoginCommand(loginDto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterCommandDTO registerDto)
        {
            var command = new RegisterCommand(registerDto);
            var result = await _mediator.Send(command);
            
            if (result)
            {
                return Ok(new { message = "Usuário registrado com sucesso" });
            }
            
            return BadRequest(new { message = "Erro ao registrar usuário" });
        }
    }
} 