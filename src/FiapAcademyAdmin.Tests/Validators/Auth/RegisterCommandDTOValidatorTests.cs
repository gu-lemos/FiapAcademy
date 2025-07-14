using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.Validators.Auth;
using FluentAssertions;

namespace FiapAcademyAdmin.Tests.Validators.Auth
{
    public class RegisterCommandDTOValidatorTests
    {
        private readonly RegisterCommandDTOValidator _validator = new();

        [Fact]
        public void Validate_DeveSerValidoQuandoTodosOsCamposEstaoCorretos()
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = "usuario@teste.com",
                Senha = "SenhaForte123!",
                ConfirmarSenha = "SenhaForte123!"
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_DeveSerInvalidoQuandoNomeEstaVazio(string? nome)
        {
            var dto = new RegisterCommandDTO
            {
                Nome = nome ?? string.Empty,
                Email = "usuario@teste.com",
                Senha = "SenhaForte123!",
                ConfirmarSenha = "SenhaForte123!"
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Nome");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_DeveSerInvalidoQuandoEmailEstaVazio(string? email)
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = email ?? string.Empty,
                Senha = "SenhaForte123!",
                ConfirmarSenha = "SenhaForte123!"
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Email");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoEmailEhInvalido()
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = "invalido",
                Senha = "SenhaForte123!",
                ConfirmarSenha = "SenhaForte123!"
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Email");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_DeveSerInvalidoQuandoSenhaEstaVazia(string? senha)
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = "usuario@teste.com",
                Senha = senha ?? string.Empty,
                ConfirmarSenha = senha ?? string.Empty
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Senha");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoSenhaEhCurta()
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = "usuario@teste.com",
                Senha = "Curta1!",
                ConfirmarSenha = "Curta1!"
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Senha");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoSenhaEhFraca()
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = "usuario@teste.com",
                Senha = "senhafraca",
                ConfirmarSenha = "senhafraca"
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Senha");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_DeveSerInvalidoQuandoConfirmacaoSenhaEstaVazia(string? confirmarSenha)
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = "usuario@teste.com",
                Senha = "SenhaForte123!",
                ConfirmarSenha = confirmarSenha ?? string.Empty
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "ConfirmarSenha");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoConfirmacaoSenhaNaoCorresponde()
        {
            var dto = new RegisterCommandDTO
            {
                Nome = "Usuário Teste",
                Email = "usuario@teste.com",
                Senha = "SenhaForte123!",
                ConfirmarSenha = "OutraSenha123!"
            };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "ConfirmarSenha");
        }
    }
} 