using FiapAcademyAdmin.Application.Validators.Turma;
using FiapAcademyAdmin.Tests.TestHelpers.Turma;
using FluentValidation.TestHelper;

namespace FiapAcademyAdmin.Tests.Validators.Turma
{
    public class CreateTurmaCommandDTOValidatorTests
    {
        private readonly CreateTurmaCommandDTOValidator _validator;

        public CreateTurmaCommandDTOValidatorTests()
        {
            _validator = new CreateTurmaCommandDTOValidator();
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoTodosOsCamposEstaoCorretos()
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_DeveSerInvalidoQuandoNomeEstaVazio(string nome)
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Nome = nome;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome é obrigatório");
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("a")]
        public void Validate_DeveSerInvalidoQuandoNomeTemMenosDe3Caracteres(string nome)
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Nome = nome;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome deve ter no mínimo 3 caracteres");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoNomeTemMaisDe100Caracteres()
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Nome = new string('a', 101);

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome deve ter no máximo 100 caracteres");
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_DeveSerInvalidoQuandoDescricaoEstaVazia(string descricao)
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Descricao = descricao;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Descricao)
                .WithErrorMessage("Descrição é obrigatória");
        }

        [Theory]
        [InlineData("123456789")]
        [InlineData("12345678")]
        [InlineData("1234567")]
        public void Validate_DeveSerInvalidoQuandoDescricaoTemMenosDe10Caracteres(string descricao)
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Descricao = descricao;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Descricao)
                .WithErrorMessage("Descrição deve ter no mínimo 10 caracteres");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoDescricaoTemMaisDe500Caracteres()
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Descricao = new string('a', 501);

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Descricao)
                .WithErrorMessage("Descrição deve ter no máximo 500 caracteres");
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoNomeTemExatamente3Caracteres()
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Nome = "abc";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoNomeTemExatamente100Caracteres()
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Nome = new string('a', 100);

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoDescricaoTemExatamente10Caracteres()
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Descricao = "1234567890";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Descricao);
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoDescricaoTemExatamente500Caracteres()
        {
            // Arrange
            var dto = TurmaTestDataBuilder.CreateTurmaCommandDTO();
            dto.Descricao = new string('a', 500);

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Descricao);
        }
    }
} 