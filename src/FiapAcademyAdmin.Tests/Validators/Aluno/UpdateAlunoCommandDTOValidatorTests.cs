using FiapAcademyAdmin.Application.Validators.Aluno;
using FiapAcademyAdmin.Tests.TestHelpers.Aluno;
using FluentValidation.TestHelper;

namespace FiapAcademyAdmin.Tests.Validators.Aluno
{
    public class UpdateAlunoCommandDTOValidatorTests
    {
        private readonly UpdateAlunoCommandDTOValidator _validator;

        public UpdateAlunoCommandDTOValidatorTests()
        {
            _validator = new UpdateAlunoCommandDTOValidator();
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoTodosOsCamposEstaoCorretos()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Validate_DeveSerInvalidoQuandoIdEhMenorOuIgualAZero(int id)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Id = id;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("ID deve ser maior que zero");
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoIdEhMaiorQueZero()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Id = 1;
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_DeveSerInvalidoQuandoNomeEstaVazio(string nome)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Nome = nome;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome é obrigatório");
        }

        [Theory]
        [InlineData("a")]
        [InlineData("aa")]
        public void Validate_DeveSerInvalidoQuandoNomeTemMenosDe3Caracteres(string nome)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Nome = nome;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome deve ter no mínimo 3 caracteres");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoNomeExcede100Caracteres()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Nome = new string('a', 101);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome deve ter no máximo 100 caracteres");
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoNomeTemExatamente3Caracteres()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Nome = "abc";
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoNomeTemExatamente100Caracteres()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Nome = new string('a', 100);
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoDataNascimentoEstaVazia()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.DataNascimento = DateTime.MinValue;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.DataNascimento)
                .WithErrorMessage("Data de nascimento é obrigatória");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoDataNascimentoEhHoje()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.DataNascimento = DateTime.Today;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.DataNascimento)
                .WithErrorMessage("Data de nascimento deve ser anterior a hoje");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoDataNascimentoEhFutura()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.DataNascimento = DateTime.Today.AddDays(1);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.DataNascimento)
                .WithErrorMessage("Data de nascimento deve ser anterior a hoje");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoDataNascimentoEhMuitoAntiga()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.DataNascimento = DateTime.Today.AddYears(-121);
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.DataNascimento)
                .WithErrorMessage("Data de nascimento inválida");
        }

        [Theory]
        [InlineData("123")]
        [InlineData("123456789")]
        public void Validate_DeveSerInvalidoQuandoCpfEstaIncorreto(string cpf)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Cpf = cpf;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Cpf)
                .WithErrorMessage("CPF deve conter 11 dígitos numéricos");
        }

        [Fact]
        public void Validate_DeveSerValidoQuandoCpfEstaCorreto()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Cpf = "12345678901";
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Cpf);
        }

        [Theory]
        [InlineData("emailinvalido")]
        [InlineData("@email.com")]
        [InlineData("email@")]
        public void Validate_DeveSerInvalidoQuandoEmailEstaIncorreto(string email)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Email = email;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("E-mail deve conter o caractere @ e ser válido");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoEmailExcede100Caracteres()
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Email = new string('a', 91) + "@email.com";
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("E-mail deve ter no máximo 100 caracteres");
        }

        [Theory]
        [InlineData("teste@email.com")]
        [InlineData("usuario@dominio.com.br")]
        [InlineData("a@b.c")]
        public void Validate_DeveSerValidoQuandoEmailEstaCorreto(string email)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Email = email;
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_DeveSerValidoQuandoSenhaEstaVazia(string senha)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Senha = senha;
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Senha);
        }

        [Theory]
        [InlineData("abcdefgh")]
        [InlineData("ABCDEFGH")]
        [InlineData("abcdefg1")]
        [InlineData("abcdefg!")]
        [InlineData("ABCDEFG1")]
        [InlineData("ABCDEFG!")]
        [InlineData("abc123!")]
        [InlineData("ABC123!")]
        public void Validate_DeveSerInvalidoQuandoSenhaNaoAtendeRequisitos(string senha)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Senha = senha;
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Senha)
                .WithErrorMessage("Senha deve conter letras maiúsculas, minúsculas, números e símbolos especiais");
        }

        [Theory]
        [InlineData("Abc123!@")]
        [InlineData("Senha123!")]
        [InlineData("MyP@ssw0rd")]
        public void Validate_DeveSerValidoQuandoSenhaAtendeRequisitos(string senha)
        {
            var dto = AlunoTestDataBuilder.UpdateAlunoCommandDTO();
            dto.Senha = senha;
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Senha);
        }
    }
} 