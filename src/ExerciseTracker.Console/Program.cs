using ExerciseTracker.Console.Configurations;
using ExerciseTracker.Console.Configurations.Enums;
using ExerciseTracker.Console.Menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddConsoleServices();

using var host = builder.Build();

var mainMenu = host.Services.GetRequiredKeyedService<IMenu>(MenuType.Main);

await mainMenu.Display();
