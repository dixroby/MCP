using MessageLogger.Options;
using Microsoft.Extensions.Logging;

namespace MessageLogger.FileLogger
{
    internal class FileLogger : BaseLogger.BaseLogger
    {
        private readonly string _fileName;

        public FileLogger(
            string categoryName, string logsDirectory, LoggerOptions options):
            base(categoryName, options)
        {
            string dir = logsDirectory;
            if (!dir.EndsWith("\\"))
                dir += "\\";
            _fileName = $"{dir}{DateTime.Now:yMdHmmss}.txt";
        }

        
        protected override void Log(string message, LogLevel logLevel, Exception exception)
        {
            File.AppendAllText(_fileName, $"{message}\n");

            if(exception != null)
            {
                File.AppendAllText(_fileName, $"Exception: {exception.ToString()}");
            }
        }
    }
}
