using System.Text.Json.Serialization;

namespace MCP.Abstractions.Models
{
    public class McpToolResult
    {
        [JsonPropertyName("content")]
        public object[] Content { get; set; }

        [JsonPropertyName("isError")]
        public bool IsError {  get; set; }

        public static McpToolResult Succes(object[] content)
            => new()
            {
                Content = content,
                IsError = false
            };

        public static McpToolResult Error(object[] content)
            => new()
            {
                Content = content,
                IsError = true
            };
    }
}
