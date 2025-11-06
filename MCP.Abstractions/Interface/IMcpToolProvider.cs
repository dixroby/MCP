using MCP.Abstractions.Models;
using System.Text.Json;

namespace MCP.Abstractions.Interface;

public interface IMcpToolProvider
{
    public string ToolName { get; }

    public Task<McpTool> GetMcpToolAsync();

    public Func<JsonElement, Task<McpToolResult>> HandleToolCallAsync { get; }
}
