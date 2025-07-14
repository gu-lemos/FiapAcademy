using FiapAcademyAdmin.Application.Interfaces.Services;
using FiapAcademyAdmin.Web.Services;

namespace FiapAcademyAdmin.Web.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IMediatorAuthService, MediatorAuthService>();
        services.AddSingleton<IAuthStateService, AuthStateService>();
        
        return services;
    }
}