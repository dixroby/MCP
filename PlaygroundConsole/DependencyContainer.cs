using LLM.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundConsole
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddGroqProvider(
            this IServiceCollection services)
        {
            services.AddOpenAiProvider(options => 
                {
                    options.BaseUrl = "https://api.groq.com/";
                    options.RelativeEndpoint = "openai/v1/chat/completions";
                    options.Model = "your model";
                    options.TimeOut = TimeSpan.FromMinutes(5);

                    options.AuthenticationHeaderName = "Authorization";
                    options.AuthenticationHeaderValue = "Bearer your token";
                });

                return services;
        }
    }

}
