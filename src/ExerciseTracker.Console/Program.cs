using ExerciseTracker.Console.Configurations;
using ExerciseTracker.Console.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddConsoleServices(builder.Configuration);

using var host = builder.Build();

var mainMenu = host.Services.GetRequiredService<MainMenu>();

await mainMenu.Display();
