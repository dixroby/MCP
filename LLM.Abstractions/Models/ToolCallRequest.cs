using System.Text.Json;

namespace LLM.Abstractions.Models
{
    //ToolCallRequest es un estructura de datos, va a envolver la pericion al LLM
    public class ToolCallRequest(List<JsonElement> conversationHistory,
                                 IEnumerable<Tool> tools = null,
                                 //ejecutar la herramienta, 
                                 // Nombre: nombre de la herramienta => obtenerClima
                                 // jsonElement: parametros q nos paso el LLM => {"city": "puebla"}
                                 // Task<string>: resultado del LLM
                                 Func<string, JsonElement, Task<string>> toolExecutor = null)
    {
        public List<JsonElement> ConversationHistory => conversationHistory;
        public IEnumerable<Tool> Tools => tools;
        public Func<string, JsonElement, Task<string>> ToolExecutor  => toolExecutor;
    }
}