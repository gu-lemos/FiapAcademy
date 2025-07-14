using FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Create;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Tests.TestHelpers.Aluno;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Aluno.Create
{
    public class CreateAlunoCommandHandlerTests
    {
        private readonly Mock<IAlunoService> _alunoServiceMock = new();
        private readonly Mock<ITurmaService> _turmaServiceMock = new();
        private readonly Mock<IMatriculaService> _matriculaServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly CreateAlunoCommandHandler _handler;

        public CreateAlunoCommandHandlerTests()
        {
            _handler = new CreateAlunoCommandHandler(
                _alunoServiceMock.Object,
                _turmaServiceMock.Object,
                _matriculaServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_DeveCriarAlunoComSucesso()
        {
            // Arrange
            var createAlunoDto = AlunoTestDataBuilder.CreateAlunoCommandDTO();
            var command = new CreateAlunoCommand { Aluno = createAlunoDto };
            var alunoEntity = new AlunoEntity(createAlunoDto.Nome, createAlunoDto.DataNascimento, createAlunoDto.Cpf, createAlunoDto.Email, createAlunoDto.Senha);
            var alunoQueryDto = AlunoTestDataBuilder.AlunoQueryDTO();

            _mapperMock.SetupMapToEntity(alunoEntity);
            _alunoServiceMock.SetupAlunoServiceForCreate(alunoEntity);
            _mapperMock.SetupMapperForQueryDTO(alunoQueryDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(alunoQueryDto);
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoCpfJaExiste()
        {
            // Arrange
            var createAlunoDto = AlunoTestDataBuilder.CreateAlunoCommandDTO();
            var command = new CreateAlunoCommand { Aluno = createAlunoDto };

            _alunoServiceMock.Setup(x => x.ExistsByCpfAsync(createAlunoDto.Cpf))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Já existe um aluno cadastrado com este CPF.");
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoEmailJaExiste()
        {
            // Arrange
            var createAlunoDto = AlunoTestDataBuilder.CreateAlunoCommandDTO();
            var command = new CreateAlunoCommand { Aluno = createAlunoDto };

            _alunoServiceMock.Setup(x => x.ExistsByCpfAsync(createAlunoDto.Cpf))
                .ReturnsAsync(false);
            _alunoServiceMock.Setup(x => x.ExistsByEmailAsync(createAlunoDto.Email))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Já existe um aluno cadastrado com este e-mail.");
        }
    }
} 