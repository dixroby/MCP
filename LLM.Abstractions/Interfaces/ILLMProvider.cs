using LLM.Abstractions.Models;
using Results;
using System.Text.Json;

namespace LLM.Abstractions.Interfaces
{
    public interface ILLMProvider
    {
        // quisiera yo saber con qué modelo estoy trabajando, como usamos DI, quiero que el proveedor me diga con que modelo trabaja
        string ModelName { get; }
        // quiero un método sendRequestAsync para que envie una petición
        // los proveedores me daran un JSON

        // esto es el contexo del LLM, cuando preguntamos por el chatgpt, todo ese chorizo se va al llm, regresa, todo el chorizo de info se va al llm, cuando la conversación crece mucho
        // se vuelve lento

        JsonElement CreateUserMessage(string mensaje);

        JsonElement CreateToolCallResultMessage(string callId, string functionName, string toolCallResult);

        Task<Result<JsonElement>> SendRequestAsync(IEnumerable<JsonElement> conversationHistory,
                                                   IEnumerable<Tool> tools = null);
    }
}
