using System.Text.Json.Serialization;

namespace MCP.Abstractions.Messages
{
    public class McpRequestMessage : McpMessage
    {
        // el metodo protocologo mcp implementa varios metodos entre ellos el initialize, initialized
        [JsonPropertyName("method")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Method { get; set; }

        // para deefinir los parametros que el metodo requiera
        [JsonPropertyName("params")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Params { get; set; }
    }
}