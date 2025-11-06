using System.Text.Json.Serialization;

namespace MCP.Abstractions.Messages;

public abstract class McpMessage
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc { get; set; } = "2.0";

    [JsonPropertyName("id")]
    public object Id { get; set; }
}