namespace MCP.Abstractions.Interface
{
    public interface IMcpTransport
    {
        Task RunAsync(CancellationToken cancellationToken= default);
    }
}
