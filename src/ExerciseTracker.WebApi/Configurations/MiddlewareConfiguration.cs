using ExerciseTracker.Infrastructure.Contexts;
using ExerciseTracker.Infrastructure.Seeding;
using Serilog;

namespace ExerciseTracker.WebApi.Configurations;

public static class MiddlewareConfiguration
{
    public static async Task<WebApplication> UseWebApplicationMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            await SeedDatabase(app);
        }

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseExceptionHandler();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }

    private static async Task SeedDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ExerciseTrackerContext>();
            context.Database.EnsureCreated();

            await SeedingService.InitializeAsync(context);
        }
        catch (Exception)
        {
            //TODO log the error
            throw;
        }

    }
}
