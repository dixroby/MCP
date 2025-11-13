using LLM.Abstractions.Interfaces;
using LLM.Abstractions.Models;
using MCP.Abstractions.Constants;
using MCP.Abstractions.Interface;
using MCP.Abstractions.Messages;
using MCP.Abstractions.Models;
using Microsoft.Extensions.Logging;
using Results;
using System.Reflection;
using System.Text.Json;

namespace PlaygroundConsole.ChatClient
{
    internal class McpClient(ILogger<McpClient> logger,
                             ILLMProvider provider,
                             IToolCaller toolCaller,
                             IMcpServer mcpServer)
    {
        readonly static JsonSerializerOptions s_jsonOptions = new()
        {
            WriteIndented = true
        };

        private List<JsonElement> _conversationHistory = new();
        private McpTool[] _mcpTools = [];
        private ToolCallRequest _request;
        private int _messageId;

        public async Task InitializeAsync()
        {
            _messageId = 1;
            await SendInitializeAsync();
            await SendInitializedAsync();
            McpResponseMessage response = await SendToolsListAsync();

            _conversationHistory = [];
            _mcpTools = GetMcpTools(response);
            _request = GetToolCallRequest(_mcpTools);
        }

        public async Task<Result<string>> SendMessageAsync(string message)
        {
            _conversationHistory.Add(provider.CreateUserMessage(message));

            Result<ToolCallResponse> callResult = await toolCaller.CallAsync(_request);

            if (callResult.IsSuccess)
            {
                return Result<string>.Ok(callResult.Value.Response);
            }

            return Result<string>.Fail(callResult.ErrorMessage);
        }
        private async Task SendInitializeAsync()
        {
            var message = new McpRequestMessage
            {
                Id = ++_messageId,
                Method = McpCommands.Initialize,
                Params = new
                {
                    protocolVersion = "2025-06-18",
                    capabilities = new { }
                }
            };

            await SendMessageAsync(message);
        }
       

        private async Task SendInitializedAsync()
        {
            var message = new McpRequestMessage
            {
                Method = McpCommands.Notification_Initialized,
                Params = new {}
            };

            await SendMessageAsync(message);
        }
        private async Task<McpResponseMessage> SendToolsListAsync()
        {
            var message = new McpRequestMessage
            {
                Id = _messageId++,
                Method = McpCommands.Tools_List,
                Params = new { }
            };

            return await SendMessageAsync(message);
        }

        private async Task<string> SendToolsCallAsync(string toolName,
                                                JsonElement arguments)
        {
            string toolCallResponse = string.Empty;

            McpRequestMessage message = new()
            {
                Id = _messageId++,
                Method = McpCommands.Tools_Call,
                Params = new McpCallToolParams
                { 
                    Name = toolName,
                    Arguments = arguments,
                }
            };

            McpResponseMessage responseMessage = await SendMessageAsync(message);

            if (responseMessage.Result != null)
            {
                // Result =
                PropertyInfo propertyInfo = 
                    responseMessage
                    .Result
                    .GetType()
                    .GetProperty("Content");

                object[] content =
                    propertyInfo?.GetValue(responseMessage) as object[];

                if (content[0] is TextContent textContent) 
                {
                    toolCallResponse = textContent.Text;
                }

            }
            else
            {
                PropertyInfo messageProperty =
                    responseMessage
                    .Error
                    .GetType()
                    .GetProperty("message");

                toolCallResponse = messageProperty?.GetValue(responseMessage.Error) as string;
            }

            return toolCallResponse;
        }

        private McpTool[] GetMcpTools (McpResponseMessage response)
        {
            PropertyInfo toolsProperty =
                    response
                    .Result
                    .GetType()
                    .GetProperty("tools");

            McpTool[] mcpTools = toolsProperty?.GetValue(response.Result) as McpTool[];
            return mcpTools;
        }
        private async Task<McpResponseMessage> SendMessageAsync(McpRequestMessage message)
        {
            logger.LogInformation("Send message to MCP SERVER:\n {message}", JsonSerializer.Serialize(message, s_jsonOptions));
            logger.LogInformation("Using {ProviderName}", provider.GetType().Name);
            logger.LogInformation("Model: {ModelName}", provider.ModelName);
            // Further implementation goes here...

            McpResponseMessage responseMessage =
                await mcpServer.HandleMessageAsync(message);

            logger.LogInformation("Received response from MCP SERVER:\n {response}", JsonSerializer.Serialize(responseMessage, s_jsonOptions));

            return responseMessage;
        }

        private ToolCallRequest GetToolCallRequest(McpTool[] mcpTools)
        {
            Tool[] tools = [];
            if (_mcpTools != null && _mcpTools.Length != 0)
            {
                tools = 
                    [
                        ..
                        _mcpTools.Select(t => new Tool {
                        Name = t.Name,
                        Description = t.Description,
                        InputSchema = t.InputSchema,
                    })];
            }
            else
                tools = [];
            return new ToolCallRequest(_conversationHistory, tools, SendToolsCallAsync);
        }
    }
}