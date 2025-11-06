namespace MCP.ServerLibrary.Options;

public class McpServerOptions
{
    public const string SectionKey = nameof(McpServerOptions);
    public string Name { get; set; }
    public string Version { get; set; }
    public string ProtocolVersion { get; set; }
}
