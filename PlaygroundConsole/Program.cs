using LLM.ToolCalling;
using MCP.ServerLibrary;
using MCP.ToolProviders;
using MessageLogger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlaygroundConsole;
using PlaygroundConsole.ChatClient;
using PlaygroundConsole.ToolCallerTests;

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


builder.Services.AddToolCaller();
builder.Services.AddSingleton<ToolCallerTest>();

builder.Services.AddMcpServer(options =>
{
    options.ProtocolVersion = "2025-06-18";
    options.Version = "1.0.0";
    options.Name = "simple mcp";
});


builder.Services.AddToolProviders();
builder.Services.AddSingleton<ChatClient>();

IHost app = builder.Build();
ChatClient test = app.Services.GetRequiredService<ChatClient>();
await test.StartAsync();