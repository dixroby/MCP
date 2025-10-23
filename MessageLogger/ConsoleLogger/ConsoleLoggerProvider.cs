using MessageLogger.Options;
using Microsoft.Extensions.Logging;

namespace MessageLogger.ConsoleLogger
{
    internal class ConsoleLoggerProvider(LoggerOptions options) : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) =>
            new ConsoleLogger(categoryName, options);

        public void Dispose()
        {
        }
    }
}
