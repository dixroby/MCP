using System.Text.Json.Serialization;

namespace LLM.Providers.OpenAi
{
    // esta es la respuesta con la cole de choices
    internal class OpenAIResponse(OpenAIChoice[] choiches)
    {
        [JsonPropertyName("choices")]
        public OpenAIChoice[] Choiches => choiches;
    }
}
