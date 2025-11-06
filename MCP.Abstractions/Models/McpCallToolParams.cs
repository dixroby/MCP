using System.Text.Json;
using System.Text.Json.Serialization;

namespace MCP.Abstractions.Models
{
    public class McpCallToolParams
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("arguments")]
        public JsonElement Arguments { get; set; }
    }
}
