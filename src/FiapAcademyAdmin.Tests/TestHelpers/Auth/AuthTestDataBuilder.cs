using FiapAcademyAdmin.Application.DTOs.Command.Auth;
using FiapAcademyAdmin.Application.DTOs.Query.Auth;
using System;

namespace FiapAcademyAdmin.Tests.TestHelpers.Auth
{
    public static class AuthTestDataBuilder
    {
        public static LoginCommandDTO LoginCommandDTO(string? email = null, string? senha = null)
            => new()
            {
                Email = email ?? "usuario@teste.com",
                Senha = senha ?? "SenhaForte123!"
            };

        public static RegisterCommandDTO RegisterCommandDTO(string? nome = null, string? email = null, string? senha = null, string? confirmarSenha = null)
            => new()
            {
                Nome = nome ?? "Usuário Teste",
                Email = email ?? "usuario@teste.com",
                Senha = senha ?? "SenhaForte123!",
                ConfirmarSenha = confirmarSenha ?? "SenhaForte123!"
            };

        public static LoginResponseDTO LoginResponseDTO(string? token = null, string? nome = null, string? email = null, string? role = null, DateTime? expiraEm = null)
            => new()
            {
                Token = token ?? "token.jwt.simulado",
                Nome = nome ?? "Usuário Teste",
                Email = email ?? "usuario@teste.com",
                Role = role ?? "User",
                ExpiraEm = expiraEm ?? DateTime.UtcNow.AddHours(1)
            };
    }
} 