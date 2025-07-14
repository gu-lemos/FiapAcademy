using AutoMapper;
using MediatR;
using FiapAcademyAdmin.Application.Models;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Application.DTOs.Query.Aluno;

namespace FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Create
{
    public class CreateAlunoCommandHandler(IAlunoService alunoService, ITurmaService turmaService, IMatriculaService matriculaService, IMapper mapper) : IRequestHandler<CreateAlunoCommand, ResultViewModel<AlunoQueryDTO>>
    {
        private readonly IAlunoService _alunoService = alunoService;
        private readonly ITurmaService _turmaService = turmaService;
        private readonly IMatriculaService _matriculaService = matriculaService;
        private readonly IMapper _mapper = mapper;

        public async Task<ResultViewModel<AlunoQueryDTO>> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
        {
            var cpfJaExiste = await _alunoService.ExistsByCpfAsync(request.Aluno.Cpf);
            if (cpfJaExiste)
            {
                return ResultViewModel<AlunoQueryDTO>.Error("Já existe um aluno cadastrado com este CPF.");
            }

            var emailJaExist = await _alunoService.ExistsByEmailAsync(request.Aluno.Email);
            if (emailJaExist)
            {
                return ResultViewModel<AlunoQueryDTO>.Error("Já existe um aluno cadastrado com este e-mail.");
            }

            var aluno = _mapper.Map<AlunoEntity>(request.Aluno);
            
            var alunoCriado = await _alunoService.CreateAsync(aluno);
            
            if (request.Aluno.TurmaIds.Count > 0)
            {
                foreach (var turmaId in request.Aluno.TurmaIds)
                {
                    var turma = await _turmaService.GetByIdAsync(turmaId);

                    if (turma == null)
                        continue;

                    var matricula = new MatriculaEntity(alunoCriado.Id, turma.Id);
                    await _matriculaService.CreateAsync(matricula);
                }
            }
            
            var result = _mapper.Map<AlunoQueryDTO>(alunoCriado);

            return ResultViewModel<AlunoQueryDTO>.Success(result);
        }
    }
} 