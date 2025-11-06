using LLM.Abstractions.Interfaces;
using LLM.Abstractions.Models;
using Results;
using Results.Extensions;
using System.Text.Json;

namespace PlaygroundConsole.ToolCallerTests
{
    // ESTE ES UN TOOL CALLER
    internal class ToolCallerTest(
        ILLMProvider provider,
        IToolCaller caller)
    {

        DecryptToolProvider _decrypt = new DecryptToolProvider();
        EncryptToolProvider _encrypt = new EncryptToolProvider();
        public async Task StartAsync()
        {
            Console.WriteLine($"Usando {provider.GetType().Name}");
            Console.WriteLine($"Modelo: {provider.ModelName}");
            Console.WriteLine();

            List<JsonElement> conversationHistory = [];

            IEnumerable<Tool> tools = [
                await WeatherToolProvider.GetToolAsync(),
                await TimeToolProvider.GetToolAsync(),
                await _decrypt.GetToolAsync(),
                await _encrypt.GetToolAsync(),
                ];

            ToolCallRequest request = new ToolCallRequest(
                conversationHistory, tools, ExecuteToolAsync);

            string userPrompt = "";

            while (userPrompt != "quit")
            {
                Console.Write("> ");
                userPrompt = Console.ReadLine();
                // YA LEYÓ LO QUE QUIERE EL USUARIO

                if (userPrompt != "quit")
                {
                    conversationHistory.Add(provider.CreateUserMessage(userPrompt));

                    Result<ToolCallResponse> callResult = await caller.CallAsync(request);

                    Console.WriteLine();

                    callResult.Match(
                        onSuccess: result => Console.WriteLine($"<<< {result.Response}"),
                        onError: result => Console.WriteLine($"Error: {result.ErrorMessage}"));

                    Console.WriteLine();
                }
            }

        }


        // gateway que redirige.
        private async Task<string> ExecuteToolAsync(
            string toolName,
            JsonElement arguments)
        {
            if (toolName == WeatherToolProvider.ToolName)
                return await WeatherToolProvider.HandleToolCallAsync(arguments);

            if (toolName == TimeToolProvider.ToolName)
                return await TimeToolProvider.HandleToolCallAsync(arguments);

            if (toolName == _decrypt.ToolName)
                return await _decrypt.HandleToolCallAsync(arguments);

            if (toolName == _encrypt.ToolName)
                return await _encrypt.HandleToolCallAsync(arguments);

            // SI LLEGO AQUÍ ES QUE NO PASÓ POR NINGUNA HERRAMIENTTAT
            return $"No executor fund for tool {toolName}";
        }


    }
}