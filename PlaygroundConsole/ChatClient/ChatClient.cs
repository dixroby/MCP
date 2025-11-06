using LLM.Abstractions.Interfaces;
using MCP.Abstractions.Interface;
using MCP.Abstractions.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaygroundConsole.ChatClient
{
    internal class ChatClient(ILLMProvider provider,
                              IMcpServer mcpServer)
    {

        readonly static JsonSerializerOptions s_jsonOptions = new()
        {
            WriteIndented = true
        };
        public async Task StartAsync()
        {
            Console.WriteLine($"Usando {provider.GetType().Name}");
            Console.WriteLine($"Modelo: {provider.ModelName}");
            Console.WriteLine();

            List<JsonElement> conversationHistory = [];

            McpRequestMessage message =
                new McpRequestMessage
                {
                    Id = 1,
                    Method = "initialize",
                    Params = new
                    {
                        protocolVersion = "2025-06-18",
                        capabilities = new { }
                    }
                };

            McpResponseMessage response =
                await mcpServer.HandleMessageAsync(message);

            string serializedResponse =
                JsonSerializer.Serialize(response, s_jsonOptions);

            Console.WriteLine($"Id; {message.Id}");
            Console.WriteLine($"Response: ");
            Console.WriteLine(serializedResponse);
        }
    }
}