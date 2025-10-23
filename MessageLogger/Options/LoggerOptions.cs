namespace MessageLogger.Options
{
    public class LoggerOptions
    {
        // agregar fecha y hora para agregar la ifno
        public bool AddDateTime { get; set; } = true;
        // information, warning, etc
        public bool AddLogLevel { get; set; } = true;
        // espacio de nombre de categoría.
        public bool AddCategory { get; set; } = true;
    }
}
