using MCP.Abstractions.Interface;
using MCP.ToolProviders.Time;
using MCP.ToolProviders.Wheather;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP.ToolProviders;

    public static class DependencyContainer
    {
        public static IServiceCollection AddToolProviders(
            this IServiceCollection services)
        {
            services.AddSingleton<IMcpToolProvider, TimeToolProvider>();
            services.AddSingleton<IMcpToolProvider, WeatherToolProvider>();

            return services;
        }
    }

