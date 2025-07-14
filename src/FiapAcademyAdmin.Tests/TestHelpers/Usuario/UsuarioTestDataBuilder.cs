using FiapAcademyAdmin.Domain.Entities;

namespace FiapAcademyAdmin.Tests.TestHelpers.Usuario
{
    public static class UsuarioTestDataBuilder
    {
        public static UsuarioEntity UsuarioEntity(string? nome = null, string? email = null, string? senha = null)
        {
            return new UsuarioEntity(
                nome ?? "Usuário Teste",
                email ?? "usuario@email.com",
                senha ?? "Senha123!"
            );
        }

        public static List<UsuarioEntity> UsuarioEntities(int count = 3)
        {
            var list = new List<UsuarioEntity>();
            for (int i = 0; i < count; i++)
            {
                list.Add(UsuarioEntity($"Usuário {i+1}", $"usuario{i+1}@email.com", "Senha123!"));
            }
            return list;
        }
    }
} 