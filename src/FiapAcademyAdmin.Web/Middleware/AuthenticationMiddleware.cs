using FiapAcademyAdmin.Application.Interfaces.Services;

namespace FiapAcademyAdmin.Web.Middleware
{
    public class AuthenticationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IAuthStateService authStateService)
        {
            var path = context.Request.Path.Value?.ToLower();
            
            var publicPages = new[]
            {
                "/login",
                "/register",
                "/_blazor",
                "/_framework",
                "/favicon.ico",
                "/css",
                "/js",
                "/lib"
            };

            var isPublicPage = publicPages.Any(page => path?.StartsWith(page) == true);
            
            if (!isPublicPage && !authStateService.IsAuthenticated)
            {
                context.Response.Redirect("/login");
                return;
            }

            await _next(context);
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
} 