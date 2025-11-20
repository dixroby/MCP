using MCP.Abstractions.Constants;
using MCP.Abstractions.Interface;
using MCP.Abstractions.Messages;
using MCP.Abstractions.Models;
using MCP.ServerLibrary.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace MCP.ServerLibrary
{
    internal class McpServer(IEnumerable<IMcpToolProvider> providers,
                             ILogger<McpServer> logger,
                             IOptions<McpServerOptions> options) : IMcpServer
    {
        private readonly static JsonSerializerOptions s_jsonOptions = new()
        {
             PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
             WriteIndented = false,
        };
        public async Task<McpResponseMessage> HandleMessageAsync(McpRequestMessage message)
        {
            try
            {
                return message.Method switch
                {
                    McpCommands.Initialize => HandleInitialize(message),
                    McpCommands.Notification_Initialized => null,
                    McpCommands.Tools_List => await HandleToolsListAsync(message),
                    McpCommands.Tools_Call => await HandleToolsCallAsync(message),
                    _ => McpResponseMessage.CreateErrorResponse(message.Id, -32601, "Method not found")

                };
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error in HandleToolCallAsync: {message}", ex.Message);

                return McpResponseMessage.CreateErrorResponse(message.Id,
                        -32603, "Internal error : {message}", ex.Message);
            }
        }

        private McpResponseMessage HandleInitialize(McpRequestMessage message)
        {
            // Definir las capacidades del servidor MCP
            var serverCapabilities = new
            {
                tools = new
                {
                    listChanged = true
                }
            };

            // Información básica del servidor
            var serverInfo = new
            {
                name = options.Value.Name,
                version = options.Value.Version,
            };

            // Construir la respuesta final con ambos objetos
            var result = new
            {
                protocolVersion = options.Value.ProtocolVersion,
                capabilities = serverCapabilities,
                serverInfo
            };

            // Retornar el mensaje de respuesta MCP
            return new McpResponseMessage
            {
                Id = message.Id,
                Result = result
            };
        }
        private async Task<McpResponseMessage> HandleToolsListAsync(McpRequestMessage message)
        {
            IEnumerable<Task<McpTool>> getToolTasks =
                providers
                .Select(p => p.GetMcpToolAsync())
                .Where(tool => tool != null);

            McpTool[] tools = 
                [
                    ..(await Task.WhenAll(getToolTasks))
                ];

            return new McpResponseMessage
            {
                Id = message.Id,
                Result = new { tools }
            };
        }

        private async Task<McpResponseMessage> HandleToolsCallAsync(McpRequestMessage message)
        {
            McpResponseMessage mcpResponseMessage = new();
            if(message.Params == null)
            {
                mcpResponseMessage =
                    McpResponseMessage
                    .CreateErrorResponse(message.Id, -32602, "Missing params");
                return mcpResponseMessage;
            }
            else
            {
                try
                {
                    string paramsJson =
                        JsonSerializer
                        .Serialize(message.Params, s_jsonOptions);
                    McpCallToolParams toolParams = 
                        JsonSerializer
                        .Deserialize<McpCallToolParams>(paramsJson, s_jsonOptions);

                    IMcpToolProvider toolProvider = null;
                    if (toolParams != null)
                    {
                        toolProvider = 
                            providers
                            .FirstOrDefault(p => p.ToolName.Equals(toolParams.Name));
                    }

                    if(toolProvider != null)
                    {
                        var toolResult = 
                            await 
                            toolProvider
                            .HandleToolCallAsync(toolParams.Arguments);

                        mcpResponseMessage = new()
                        {
                            Id = message.Id,
                            Result = toolResult
                        };
                    }
                    else
                    {
                        mcpResponseMessage = 
                            McpResponseMessage
                            .CreateErrorResponse(message.Id,
                                                 -32602,
                                                 "Tool not found");
                    }
                    return mcpResponseMessage;
                }
                catch (Exception ex)
                {
                    logger
                        .LogInformation("Error in HandleToolCallAsync: {message}", ex.Message);
                    
                    mcpResponseMessage = 
                        McpResponseMessage
                        .CreateErrorResponse(message.Id,
                                             -32603,
                                             $"Internal error : {ex.Message}");
                    return mcpResponseMessage;
                }
            }
        }
    }

   
}
