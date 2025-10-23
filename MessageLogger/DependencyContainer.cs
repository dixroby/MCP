using MessageLogger.ConsoleLogger;
using MessageLogger.FileLogger;
using MessageLogger.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MessageLogger
{
    public static class DependencyContainer
    {
        public static ILoggingBuilder AddConsoleLogger(
            this ILoggingBuilder builder,
            Action<LoggerOptions> configure = null)
        {
            LoggerOptions options = new();
            if (configure != null)
                configure(options);

            builder.AddProvider(new ConsoleLoggerProvider(options));

            return builder;
        }


        public static ILoggingBuilder AddFileLogger(
            this ILoggingBuilder builder,
            string logsDirectory,
            Action<LoggerOptions> configure = null)
        {
            LoggerOptions options = new();
            if (configure != null)
                configure(options);

            builder.AddProvider(new FileLoggerProvider(logsDirectory, options));

            return builder;
        }


    }

}
