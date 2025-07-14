using FiapAcademyAdmin.Application.Handlers.Commands.Aluno.Update;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Tests.TestHelpers.Aluno;
using FluentAssertions;
using Moq;

namespace FiapAcademyAdmin.Tests.Handlers.Commands.Aluno.Update
{
    public class UpdateAlunoCommandHandlerTests
    {
        private readonly Mock<IAlunoService> _alunoServiceMock = new();
        private readonly Mock<ITurmaService> _turmaServiceMock = new();
        private readonly Mock<IMatriculaService> _matriculaServiceMock = new();
        private readonly Mock<AutoMapper.IMapper> _mapperMock = new();
        private readonly UpdateAlunoCommandHandler _handler;

        public UpdateAlunoCommandHandlerTests()
        {
            _handler = new UpdateAlunoCommandHandler(
                _alunoServiceMock.Object,
                _turmaServiceMock.Object,
                _matriculaServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_DeveAtualizarAlunoComSucesso()
        {
            // Arrange
            var updateAlunoDto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            var command = new UpdateAlunoCommand { Aluno = updateAlunoDto };
            var alunoAtual = new AlunoEntity(updateAlunoDto.Nome, updateAlunoDto.DataNascimento, updateAlunoDto.Cpf, updateAlunoDto.Email, updateAlunoDto.Senha!);
            alunoAtual.DefinirId(updateAlunoDto.Id);
            var alunoAtualizado = new AlunoEntity(updateAlunoDto.Nome, updateAlunoDto.DataNascimento, updateAlunoDto.Cpf, updateAlunoDto.Email, updateAlunoDto.Senha!);
            var alunoQueryDto = AlunoTestDataBuilder.AlunoQueryDTO();

            _alunoServiceMock.SetupAlunoServiceForUpdate(alunoAtualizado);
            _matriculaServiceMock.Setup(s => s.GetByAlunoIdAsync(updateAlunoDto.Id)).ReturnsAsync(new List<MatriculaEntity>());
            _mapperMock.SetupMapperForQueryDTO(alunoQueryDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(alunoQueryDto);
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoAlunoNaoEncontrado()
        {
            // Arrange
            var updateAlunoDto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            var command = new UpdateAlunoCommand { Aluno = updateAlunoDto };
            
            _alunoServiceMock.Setup(s => s.GetByIdAsync(updateAlunoDto.Id)).ReturnsAsync((AlunoEntity?)null);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Aluno não encontrado");
            result.Data.Should().BeNull();
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoCpfJaExisteEmOutroAluno()
        {
            // Arrange
            var updateAlunoDto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            var command = new UpdateAlunoCommand { Aluno = updateAlunoDto };
            var alunoAtual = new AlunoEntity("Nome Original", DateTime.Today.AddYears(-20), "12345678901", "email@original.com", "senha123");
            alunoAtual.DefinirId(updateAlunoDto.Id);

            _alunoServiceMock.Setup(s => s.GetByIdAsync(updateAlunoDto.Id)).ReturnsAsync(alunoAtual);
            _alunoServiceMock.Setup(x => x.ExistsByCpfAsync(updateAlunoDto.Cpf))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Já existe um aluno cadastrado com este CPF.");
        }

        [Fact]
        public async Task Handle_DeveRetornarErroQuandoEmailJaExisteEmOutroAluno()
        {
            // Arrange
            var updateAlunoDto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            var command = new UpdateAlunoCommand { Aluno = updateAlunoDto };
            var alunoAtual = new AlunoEntity("Nome Original", DateTime.Today.AddYears(-20), "12345678901", "email@original.com", "senha123");
            alunoAtual.DefinirId(updateAlunoDto.Id);

            _alunoServiceMock.Setup(s => s.GetByIdAsync(updateAlunoDto.Id)).ReturnsAsync(alunoAtual);
            _alunoServiceMock.Setup(x => x.ExistsByCpfAsync(updateAlunoDto.Cpf))
                .ReturnsAsync(false);
            _alunoServiceMock.Setup(x => x.ExistsByEmailAsync(updateAlunoDto.Email))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Já existe um aluno cadastrado com este e-mail.");
        }

        [Fact]
        public async Task Handle_DevePermitirAtualizacaoQuandoCpfNaoMudou()
        {
            // Arrange
            var updateAlunoDto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            var command = new UpdateAlunoCommand { Aluno = updateAlunoDto };
            var alunoAtual = new AlunoEntity(updateAlunoDto.Nome, updateAlunoDto.DataNascimento, updateAlunoDto.Cpf, "email@diferente.com", updateAlunoDto.Senha!);
            alunoAtual.DefinirId(updateAlunoDto.Id);
            var alunoAtualizado = new AlunoEntity(updateAlunoDto.Nome, updateAlunoDto.DataNascimento, updateAlunoDto.Cpf, updateAlunoDto.Email, updateAlunoDto.Senha!);
            var alunoQueryDto = AlunoTestDataBuilder.AlunoQueryDTO();

            _alunoServiceMock.Setup(s => s.GetByIdAsync(updateAlunoDto.Id)).ReturnsAsync(alunoAtual);
            _alunoServiceMock.SetupAlunoServiceForUpdate(alunoAtualizado);
            _matriculaServiceMock.Setup(s => s.GetByAlunoIdAsync(updateAlunoDto.Id)).ReturnsAsync(new List<MatriculaEntity>());
            _mapperMock.SetupMapperForQueryDTO(alunoQueryDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(alunoQueryDto);
        }
    }
} 