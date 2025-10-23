using MessageLogger.Options;
using Microsoft.Extensions.Logging;

namespace MessageLogger.ConsoleLogger
{
    internal class ConsoleLogger(string categoryName, LoggerOptions options) :
        BaseLogger.BaseLogger(categoryName, options)
    {
        protected override void Log(string message, LogLevel logLevel, Exception exception)
        {
            Console.ForegroundColor = GetLogLevelColor(logLevel);
            Console.WriteLine(message);
            if(exception != null)
            {
                Console.WriteLine($"Exception: {exception}");
            }
            Console.ResetColor();
        }

        private static ConsoleColor GetLogLevelColor(LogLevel logLevel) =>
            logLevel switch
            {
                LogLevel.Trace => ConsoleColor.Gray,
                LogLevel.Debug => ConsoleColor.DarkGray,
                LogLevel.Information => ConsoleColor.White,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Critical => ConsoleColor.DarkRed,
                _ => ConsoleColor.White
            };
    }
}
