using FiapAcademyAdmin.API.Middleware;

namespace FiapAcademyAdmin.API.Extensions
{
    public static class ApiApplicationExtensions
    {
        public static WebApplication ConfigureApiPipeline(this WebApplication app)
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
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
} 