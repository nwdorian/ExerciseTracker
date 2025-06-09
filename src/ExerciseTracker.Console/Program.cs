using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

using var host = builder.Build();
await host.RunAsync();
