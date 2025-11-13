using LLM.Abstractions.Interfaces;
using LLM.Abstractions.Models;
using MCP.Abstractions.Interface;
using MCP.Abstractions.Messages;
using Results;
using Results.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaygroundConsole.ChatClient
{
    internal class ChatClient(ILLMProvider provider,
                              McpClient client)
    {
        public async Task StartAsync()
        {
            Console.WriteLine($"Usando {provider.GetType().Name}");
            Console.WriteLine($"Modelo: {provider.ModelName}");
            Console.WriteLine();

            await client.InitializeAsync();

            string userPrompt = "";

            while (userPrompt != "quit")
            {
                Console.Write("> ");
                userPrompt = Console.ReadLine();

                if (userPrompt != "quit")
                {
                    Result<string> callResult =
                    await client.SendMessageAsync(userPrompt);
                    Console.WriteLine();

                    callResult.Match(
                        
                        onSuccess: result => Console.WriteLine($"<<< {result}"),
                        onError: result => Console.WriteLine($"Error: {result.ErrorMessage}"));

                    Console.WriteLine();
                }
            }
        }
    }
}