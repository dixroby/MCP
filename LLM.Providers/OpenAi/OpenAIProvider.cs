using LLM.Abstractions.LLMProviderOptions;
using LLM.Abstractions.Resources;
using LLM.Providers.BaseProvider;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Results;
using System.Text.Json;

namespace LLM.Providers.OpenAi
{
    internal class OpenAIProvider(
        IHttpClientFactory factory,
        IOptions<LLMProviderOptions> options,
        ILogger<OpenAIProvider> logger
        ) : BaseLLMProvider(factory, options, logger)
    {

        // CONSTRUIR EL CUERPO DE LA WEA, con el contexto
        protected override StringContent BuildRequestBody(IEnumerable<JsonElement> conversationHistory)
        {
            // esto nos lo da la docu del proveedor, lo que quiere que le envieemos
            Dictionary<string, object> requestBody = new Dictionary<string, object>()
            {
                ["model"] = lLMProviderOptions.Model,
                // Algunos modelos te devuelven la respuesta en stream
                ["stream"] = false,
                ["messages"] = conversationHistory, // incluye quien es, el usuario: lo que pregunto; asistente: respuesta
                ["temperature"] = 0.5,
                ["max_completion_tokens"] = 4096
            };

            string json = JsonSerializer.Serialize(requestBody, JsonOptions);

            return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }

        protected override Result<JsonElement> ProcessResponse(string responseContent)
        {
            OpenAIResponse openAIResponse = 
                JsonSerializer.Deserialize<OpenAIResponse>(responseContent);

            return GetResponseMessage(openAIResponse);
        }

        protected override void ConfigureHeader(HttpClient client)
        {
            // agreggar header
            client.DefaultRequestHeaders.Add(
                lLMProviderOptions.AuthenticationHeaderName, 
                lLMProviderOptions.AuthenticationHeaderValue);
        }
        
        private static Result<JsonElement> GetResponseMessage(OpenAIResponse response)
        {
            Result<JsonElement> result;

            if(response.Choiches?.Length > 0)
            {
                JsonElement message = response.Choiches[0].Message;
                result = Result<JsonElement>.Ok(message);
            }
            else
            {
                result = Result<JsonElement>.Fail(
                    Messages.NoChoicesReturnedFromAPI);
            }

            return result;
        }
    }
}
