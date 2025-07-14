using FiapAcademyAdmin.Domain.Entities;
using FiapAcademyAdmin.Infrastructure.Data;
using FiapAcademyAdmin.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FiapAcademyAdmin.Tests.Infrastructure.Repositories
{
    public class UsuarioRepositoryTests
    {
        private static ApplicationDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarUsuario()
        {
            using var context = CreateInMemoryContext();
            var repo = new UsuarioRepository(context);
            var usuario = new UsuarioEntity("Usuário Teste", "usuario@email.com", "Senha123!");

            var result = await repo.AddAsync(usuario);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            (await context.Usuarios.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarUsuarioExistente()
        {
            using var context = CreateInMemoryContext();
            var repo = new UsuarioRepository(context);
            var usuario = new UsuarioEntity("Usuário Teste", "usuario@email.com", "Senha123!");
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var result = await repo.GetByIdAsync(usuario.Id);

            result.Should().NotBeNull();
            result!.Nome.Should().Be("Usuário Teste");
        }

        [Fact]
        public async Task GetByEmailAsync_DeveRetornarUsuarioPorEmail()
        {
            using var context = CreateInMemoryContext();
            var repo = new UsuarioRepository(context);
            var usuario = new UsuarioEntity("Usuário Teste", "usuario@email.com", "Senha123!");
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var result = await repo.GetByEmailAsync("usuario@email.com");

            result.Should().NotBeNull();
            result!.Email.Should().Be("usuario@email.com");
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarTodosUsuarios()
        {
            using var context = CreateInMemoryContext();
            var repo = new UsuarioRepository(context);
            context.Usuarios.Add(new UsuarioEntity("Usuário1", "u1@email.com", "Senha123!"));
            context.Usuarios.Add(new UsuarioEntity("Usuário2", "u2@email.com", "Senha123!"));
            await context.SaveChangesAsync();

            var result = await repo.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarUsuario()
        {
            using var context = CreateInMemoryContext();
            var repo = new UsuarioRepository(context);
            var usuario = new UsuarioEntity("Usuário Teste", "usuario@email.com", "Senha123!");
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var usuarioAtualizado = new UsuarioEntity("Novo Nome", "usuario@email.com", "NovaSenha123!");
            var result = await repo.UpdateAsync(usuarioAtualizado);

            result.Should().NotBeNull();
            result.Nome.Should().Be("Novo Nome");
            result.Senha.Should().Be("NovaSenha123!");
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverUsuario()
        {
            using var context = CreateInMemoryContext();
            var repo = new UsuarioRepository(context);
            var usuario = new UsuarioEntity("Usuário Teste", "usuario@email.com", "Senha123!");
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var result = await repo.DeleteAsync(usuario.Id);

            result.Should().BeTrue();
            (await context.Usuarios.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task ExistsByEmailAsync_DeveRetornarTrueSeEmailExistir()
        {
            using var context = CreateInMemoryContext();
            var repo = new UsuarioRepository(context);
            var usuario = new UsuarioEntity("Usuário Teste", "usuario@email.com", "Senha123!");
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var result = await repo.ExistsByEmailAsync("usuario@email.com");
            result.Should().BeTrue();
        }
    }
} 