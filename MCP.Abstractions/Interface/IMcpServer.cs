using MCP.Abstractions.Messages;

namespace MCP.Abstractions.Interface
{
    public interface IMcpServer
    {
        Task<McpResponseMessage> HandleMessageAsync(McpRequestMessage message);
    }
}
