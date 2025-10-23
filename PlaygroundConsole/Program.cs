using MessageLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlaygroundConsole;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddGroqProvider();
builder.Services.AddHttpClient();

builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddConsoleLogger(configure =>
    {
        configure.AddLogLevel = false;
        configure.AddCategory = false;
    });
});

builder.Services.AddSingleton<LLMProviderTests>();

IHost app = builder.Build();

LLMProviderTests test = app.Services.GetRequiredService<LLMProviderTests>();
await test.StartAsync();
