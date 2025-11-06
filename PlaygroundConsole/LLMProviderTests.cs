using LLM.Abstractions.Extensions;
using LLM.Abstractions.Interfaces;
using Results;
using Results.Extensions;
using System.Text.Json;

namespace PlaygroundConsole
{
    internal class LLMProviderTests( ILLMProvider provider)
    {
        public async Task StartAsync()
        {
            Console.WriteLine($"Usando: {provider.GetType().Name}");
            Console.WriteLine($"Modelo: {provider.ModelName}");
            Console.WriteLine();

            List<JsonElement> conversationHistory = [];

            string userPromp = "";

            while (userPromp != "quit")
            {
                Console.Write("> ");
                userPromp = Console.ReadLine();
                if (userPromp != "quit")
                {
                    conversationHistory
                        .Add(provider.CreateUserMessage(userPromp).ToJsonElement());

                    Result<JsonElement> callResult =
                        await
                        provider
                        .SendRequestAsync(conversationHistory);

                    Console.WriteLine();

                    callResult.Match(
                        onSuccess: result => Console.WriteLine( result.GetProperty("content").GetString()),
                        onError: result => Console.WriteLine($"Error: {result.ErrorMessage}"));

                    Console.WriteLine();

                }
            }
        }
    }
}
