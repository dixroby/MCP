using System.Text.Json;
using System.Text.Json.Serialization;

namespace LLM.Providers.OpenAi
{
    // como parte de la petición, viene en una estructura Json, el LLM te va a responder, pero no es como en Chat gpt, me regresa un objeto Json.
    // me regresa lo siguiente, me responde el proveedor
    internal class OpenAIChoice(int index, JsonElement message, string finishReason)
    {
        [JsonPropertyName("index")]
        public int Index => index;
        [JsonPropertyName("message")]
        public JsonElement Message => message;
        [JsonPropertyName("finish_reason")]
        public string FinishReason => finishReason;
    }
}
