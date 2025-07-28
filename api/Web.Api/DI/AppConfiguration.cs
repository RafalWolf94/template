using Web.Api.TechnicalStuff.Error;

namespace Web.Api.DI;

public static class BuildAppExtension
{
    public static void BuildApp(this WebApplication app)
    {
        app.UseExceptionHandler(error => error.UseAppExceptionPolicy());
        app.UseCors(options =>
        {
            options.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://obliczony.pl")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}