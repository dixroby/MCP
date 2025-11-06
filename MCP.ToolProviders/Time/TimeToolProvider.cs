using MCP.Abstractions.Interface;
using MCP.Abstractions.Models;
using System.Text.Json;

namespace MCP.ToolProviders.Time;

internal class TimeToolProvider : IMcpToolProvider
{
    private static McpTool s_TTimeTool =>
            new()
            {
                Name = "get_time",
                Description = "Get the current time right now",
                InputSchema = new
                {
                    type = "object",
                    properties = new { },
                    required = Array.Empty<string>(),
                }
            };

    public string ToolName => s_TTimeTool.Name;

    public  Task<McpTool> GetMcpToolAsync() =>
           Task.FromResult(s_TTimeTool);

    

    public  Func<JsonElement, Task<McpToolResult>> HandleToolCallAsync =>
            args =>
                Task.FromResult(
                    
                    McpToolResult.Succes(
                        [
                         new TextContent {
                             Text = DateTime.Now.ToString("yyy-MM-dd HH:mm:sss")
                         }
                        ])
                    );


    
}
