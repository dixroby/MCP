using MessageLogger.Options;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MessageLogger.BaseLogger
{
    internal abstract class BaseLogger(
        string categoryName, LoggerOptions options) : ILogger
    {
        // un método que retorna dispose, no lo necesito pero me obliga la interfaz
        public IDisposable BeginScope<TState>(TState state) => null;

        // metodo para ver si el loger está habilitado
        // este log level está habilitado? puedo desactivar loglevel de debug, error, etc, cuando quiera hacer un log se invocará este metodod y pregunta si está habilitado
        public bool IsEnabled(LogLevel logLevel) => true;

        // recibe el nivel de log, un id de evento, recibe el estado (msg a escribir), una execption, un formateador que recibe un tstate, exception y retorna un string

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, 
            Exception exception, Func<TState, Exception, string> formatter)
        {

            if (!IsEnabled(logLevel))
                return;
            // formateador y crear el mensaje
            string originalMessage = formatter(state, exception);

            StringBuilder sb = new StringBuilder();
            // si quiero agregar fecha y hora
            if (options.AddDateTime)
                sb.Append($"[{DateTime.Now:H:mm:ss}]");

            // trace, debug, error, critial, esos son los levels, el log level lo quiero una longitud fija, trace, debug con 5, pero information tiene 12, cuando se escriban los logs se desaliniaran, algo que 
            // haré es que pondré un método privado estático que retorne un string
            if (options.AddLogLevel)
                sb.Append($"[{GetLogLevelAlias(logLevel)}]");

            if (options.AddCategory)
                sb.Append($"[{categoryName}]");

            sb.Append(originalMessage);
            // fecha por el alias y por el category name o nada de eso.pero quisiera al archivo o la consola.

            Log(sb.ToString(), logLevel, exception);
        }

        // metodo a implementar para implementar según el medio
        protected abstract void Log(
            string message, LogLevel logLevel, Exception exception);



        private static string GetLogLevelAlias(LogLevel logLevel) =>
            logLevel switch
            {
                LogLevel.Trace => "TRA",
                LogLevel.Debug => "DEB",
                LogLevel.Information => "INF",
                LogLevel.Warning => "WAR",
                LogLevel.Error => "ERR",
                LogLevel.Critical => "CRI",
                _ => "UNK"
            };




    }
}
