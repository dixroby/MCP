using LLM.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LLM.ToolCalling
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddToolCaller(this IServiceCollection services)
        {
            services.AddScoped<IToolCaller, ToolCaller>();

            return services;
        }
    }
}
