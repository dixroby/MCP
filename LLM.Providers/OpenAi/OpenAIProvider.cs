using LLM.Abstractions.Extensions;
using LLM.Abstractions.LLMProviderOptions;
using LLM.Abstractions.Models;
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
        protected override StringContent BuildRequestBody(IEnumerable<JsonElement> conversationHistory,
                                                          IEnumerable<Tool> tools)
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

            if (tools != null && tools.Any())
            {
                requestBody["tools"] = ConvertToolsToFunctions(tools);
                // le damos la responsabilidad de ejecución a la llm para que ejecute la herramienta
                requestBody["tool_choice"] = "auto";
            }

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

            if (response.Choiches?.Length > 0)
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

        public override JsonElement CreateUserMessage(string message)
        =>
            JsonSerializer.SerializeToElement(new
            {
                role = "user",
                content = message
            });

        public override JsonElement CreateToolCallResultMessage(string callId, string functionName, string toolCallResult)
        =>
            new OpenAIToolCallResult("tool", callId, toolCallResult).ToJsonElement();
    }
}
