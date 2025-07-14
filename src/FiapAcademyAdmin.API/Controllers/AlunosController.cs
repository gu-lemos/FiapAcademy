using FiapAcademyAdmin.API.Filters;
using FiapAcademyAdmin.Application.DTOs.Command.Aluno;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;
using FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Create;
using FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Delete;
using FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Update;
using FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetAll;
using FiapAcademyAdmin.Application.Handlers.Queries.Aluno.GetById;
using FiapAcademyAdmin.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FiapAcademyAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [JwtAuthorize]
    public class AlunosController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(ResultViewModel<AlunoListQueryDTO>), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ResultViewModel<AlunoListQueryDTO>>> Get(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10,
            [FromQuery] string? filtro = null)
        {
            var query = new GetAlunosQuery 
            { 
                Page = page, 
                PageSize = pageSize,
                Filtro = filtro
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultViewModel<AlunoQueryDTO>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ResultViewModel<AlunoQueryDTO>), 404)]
        public async Task<ActionResult<ResultViewModel<AlunoQueryDTO>>> GetById(int id)
        {
            var query = new GetAlunoByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (!result.IsSuccess)
                return NotFound(result);
                
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultViewModel<AlunoQueryDTO>), 201)]
        [ProducesResponseType(typeof(ResultViewModel<AlunoQueryDTO>), 400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<ResultViewModel<AlunoQueryDTO>>> Create([FromBody] CreateAlunoCommandDTO dto)
        {
            var command = new CreateAlunoCommand { Aluno = dto };
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
                return BadRequest(result);
                
            return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResultViewModel<AlunoQueryDTO>), 200)]
        [ProducesResponseType(typeof(ResultViewModel<AlunoQueryDTO>), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(ResultViewModel<AlunoQueryDTO>), 404)]
        public async Task<ActionResult<ResultViewModel<AlunoQueryDTO>>> Update(int id, [FromBody] UpdateAlunoCommandDTO dto)
        {
            if (id != dto.Id)
                return BadRequest(ResultViewModel<AlunoQueryDTO>.Error("ID da rota não corresponde ao ID fornecido no body"));
                
            var command = new UpdateAlunoCommand { Aluno = dto };
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
            var command = new DeleteAlunoCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result.IsSuccess)
                return NotFound(result);
                
            return Ok(result);
        }
    }
} 