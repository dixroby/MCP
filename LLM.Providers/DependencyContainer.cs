using LLM.Abstractions.Interfaces;
using LLM.Abstractions.LLMProviderOptions;
using LLM.Providers.OpenAi;
using Microsoft.Extensions.DependencyInjection;

namespace LLM.Providers
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddOpenAiProvider(
            this IServiceCollection services,
            Action<LLMProviderOptions> configureProviderOptions)
        {
            services.AddSingleton<ILLMProvider, OpenAIProvider>();
            services.Configure(configureProviderOptions);


            return services;
        }
    }

}
