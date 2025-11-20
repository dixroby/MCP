using LLM.Providers;
using Microsoft.Extensions.DependencyInjection;

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
                    options.Model = "moonshotai/kimi-k2-instruct-0905";
                    options.TimeOut = TimeSpan.FromMinutes(5);

                    options.AuthenticationHeaderName = "Authorization";
                    options.AuthenticationHeaderValue = "Bearer API_KEY";
                });

            return services;
        }
    }

}
