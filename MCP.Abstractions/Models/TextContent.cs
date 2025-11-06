using System.Text.Json.Serialization;

namespace MCP.Abstractions.Models
{
    public class TextContent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "text";

        [JsonPropertyName("type")]
        public string Text { get; set; }
    }
}
