using ExerciseTracker.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApplicationServices(builder.Configuration);

var app = builder.Build();

await app.UseWebApplicationMiddleware();

app.Run();
