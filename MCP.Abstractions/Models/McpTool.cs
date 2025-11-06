using System.Text.Json.Serialization;

namespace MCP.Abstractions.Models
{
    public class McpTool
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("inputSchema")]
        public object InputSchema { get; set; }

        [JsonPropertyName("outputSchema")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string outputSchema { get; set; }
    }
}