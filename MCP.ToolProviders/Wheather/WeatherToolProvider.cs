using MCP.Abstractions.Interface;
using MCP.Abstractions.Models;
using System.Text.Json;

namespace MCP.ToolProviders.Wheather;

internal class WeatherToolProvider : IMcpToolProvider
{
    private static McpTool s_weatherTool => new()
    {
        Name = "get_weather",
        Description = "Get current weather information for a specific city",
        InputSchema = new
        {
            type = "object",
            properties = new
            {
                city = new
                {
                    type = "string",
                    description = "Name of the city"
                },

            },
            required = new[] { "city" }
        }
    };

    public string ToolName => s_weatherTool.Name;

    public Func<JsonElement, Task<McpToolResult>> HandleToolCallAsync => args =>
    {
        McpToolResult result;

        try
        {
            string city = args.GetProperty("city").ToString();
            result = McpToolResult.Succes(
                [
                    new TextContent {
                        Text = $"En {city} está soleado con 25 grados centígrados",
                    }
                ]);

        }
        catch (Exception ex)
        {
            result = McpToolResult.Error(
                [
                    new TextContent {
                        Text =  $"Error obteniendo el clima: {ex.Message}"
                    }
                ]);
        }

        return Task.FromResult(result);
    };

    public Task<McpTool> GetMcpToolAsync() =>
        Task.FromResult(s_weatherTool);

}