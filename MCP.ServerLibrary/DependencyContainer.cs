using MCP.Abstractions.Interface;
using MCP.ServerLibrary.Options;
using Microsoft.Extensions.DependencyInjection;

namespace MCP.ServerLibrary
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddMcpServer(
            this IServiceCollection services,
            Action<McpServerOptions> configureServerOptions)
        {
            services.AddSingleton<IMcpServer, McpServer>();
            services.Configure(configureServerOptions);

            return services;
        }
    }
}
