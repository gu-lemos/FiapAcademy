using FiapAcademyAdmin.Application.Interfaces.Repositories;
using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Infrastructure.Data;
using FiapAcademyAdmin.Infrastructure.Repositories;
using FiapAcademyAdmin.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FiapAcademyAdmin.Infrastructure.Extensions;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("FiapAcademyDb"));

        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<ITurmaService, TurmaService>();
        services.AddScoped<IMatriculaService, MatriculaService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IAlunoRepository, AlunoRepository>();
        services.AddScoped<ITurmaRepository, TurmaRepository>();
        services.AddScoped<IMatriculaRepository, MatriculaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }

    public static void SeedData(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.SeedData();
    }
} 