using FiapAcademyAdmin.Web.Components;
using FiapAcademyAdmin.Web.Middleware;

namespace FiapAcademyAdmin.Web.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication ConfigureWebPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthenticationMiddleware();

            app.UseAntiforgery();

            return app;
        }

        public static WebApplication ConfigureEndpoints(this WebApplication app)
        {
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            return app;
        }
    }
} 