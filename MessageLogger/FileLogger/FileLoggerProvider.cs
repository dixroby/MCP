using MessageLogger.Options;
using Microsoft.Extensions.Logging;

namespace MessageLogger.FileLogger
{
    internal class FileLoggerProvider(string logsDirectory, LoggerOptions options) : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName, logsDirectory, options);
        }

        public void Dispose()
        {
        }
    }
}
