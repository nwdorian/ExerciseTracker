using ExerciseTracker.Console.Configurations;
using ExerciseTracker.Console.Menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddConsoleServices();

using var host = builder.Build();

var mainMenu = host.Services.GetRequiredService<MainMenu>();

await mainMenu.Display();
