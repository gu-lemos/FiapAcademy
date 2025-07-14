using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.Validators.Auth;
using FluentAssertions;

namespace FiapAcademyAdmin.Tests.Validators.Auth
{
    public class LoginCommandDTOValidatorTests
    {
        private readonly LoginCommandDTOValidator _validator = new();

        [Fact]
        public void Validate_DeveSerValidoQuandoTodosOsCamposEstaoCorretos()
        {
            var dto = new LoginCommandDTO { Email = "usuario@teste.com", Senha = "SenhaForte123!" };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_DeveSerInvalidoQuandoEmailEstaVazio(string? email)
        {
            var dto = new LoginCommandDTO { Email = email ?? string.Empty, Senha = "SenhaForte123!" };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Email");
        }

        [Fact]
        public void Validate_DeveSerInvalidoQuandoEmailEhInvalido()
        {
            var dto = new LoginCommandDTO { Email = "invalido", Senha = "SenhaForte123!" };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Email");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Validate_DeveSerInvalidoQuandoSenhaEstaVazia(string? senha)
        {
            var dto = new LoginCommandDTO { Email = "usuario@teste.com", Senha = senha ?? string.Empty };
            var result = _validator.Validate(dto);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.PropertyName == "Senha");
        }
    }
} 