using FiapAcademyAdmin.API.Filters;
using FiapAcademyAdmin.Application.DTOs.Command.Turma;
using FiapAcademyAdmin.Application.DTOs.Query.Turma;
using FiapAcademyAdmin.Application.Handlers.Commands.Turma.Create;
using FiapAcademyAdmin.Application.Handlers.Commands.Turma.Delete;
using FiapAcademyAdmin.Application.Handlers.Commands.Turma.Update;
using FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetAll;
using FiapAcademyAdmin.Application.Handlers.Queries.Turma.GetById;
using FiapAcademyAdmin.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FiapAcademyAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtAuthorize]
    public class TurmasController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(ResultViewModel<TurmaListQueryDTO>), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ResultViewModel<TurmaListQueryDTO>>> Get(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10,
            [FromQuery] string? filtro = null)
        {
            var query = new GetTurmasQuery 
            { 
                Page = page, 
                PageSize = pageSize,
                Filtro = filtro
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultViewModel<TurmaQueryDTO>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ResultViewModel<TurmaQueryDTO>), 404)]
        public async Task<ActionResult<ResultViewModel<TurmaQueryDTO>>> GetById(int id)
        {
            var query = new GetTurmaByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (!result.IsSuccess)
                return NotFound(result);
                
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultViewModel<TurmaQueryDTO>), 201)]
        [ProducesResponseType(typeof(ResultViewModel<TurmaQueryDTO>), 400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ResultViewModel<TurmaQueryDTO>>> Create([FromBody] CreateTurmaCommandDTO dto)
        {
            var command = new CreateTurmaCommand { Turma = dto };
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultViewModel<TurmaQueryDTO>), 200)]
        [ProducesResponseType(typeof(ResultViewModel<TurmaQueryDTO>), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ResultViewModel<TurmaQueryDTO>), 404)]
        public async Task<ActionResult<ResultViewModel<TurmaQueryDTO>>> Update(int id, [FromBody] UpdateTurmaCommandDTO dto)
        {
            if (id != dto.Id)
                return BadRequest(ResultViewModel<TurmaQueryDTO>.Error("ID da rota não corresponde ao ID fornecido no body"));
                
            var command = new UpdateTurmaCommand { Turma = dto };
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
                return NotFound(result);
                
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResultViewModel<bool>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ResultViewModel<bool>), 404)]
        public async Task<ActionResult<ResultViewModel<bool>>> Delete(int id)
        {
            var command = new DeleteTurmaCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
                return NotFound(result);
                
            return Ok(result);
        }
    }
} 