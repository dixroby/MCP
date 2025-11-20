using System.Text.Json.Serialization;

namespace MCP.Abstractions.Models
{
    public class TextContent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "text";

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
