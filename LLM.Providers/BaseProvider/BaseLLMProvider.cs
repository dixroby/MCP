using LLM.Abstractions.Interfaces;
using LLM.Abstractions.LLMProviderOptions;
using LLM.Abstractions.Resources;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Results;
using System.Text.Json;

namespace LLM.Providers.BaseProvider
{
    internal abstract class BaseLLMProvider : ILLMProvider
    {

        protected static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }; // NORMALMENTE LA COMUNICACIÓN CON los proveedores es con calme case, las politicas es serializar en camel case.

        // todos los proveedores se comunican con http
        protected readonly HttpClient Client;

        // van a tener opciones
        protected readonly LLMProviderOptions lLMProviderOptions;

        protected readonly ILogger Logger;

        // Esto es una clase abstracta, una vez se ha creado la instancia, debemos configurar
        // el encabezado para colocar la autorización
        protected BaseLLMProvider(IHttpClientFactory factory,
            IOptions<LLMProviderOptions> options, ILogger logger)
        {
            lLMProviderOptions = options.Value;
            Logger = logger;
            Client = factory.CreateClient();
            Client.BaseAddress = new Uri(lLMProviderOptions.BaseUrl);
            // este es util con ollama
            Client.Timeout = lLMProviderOptions.TimeOut.Value;

            ConfigureHeader(Client);
        }

        public string ModelName => lLMProviderOptions.Model;

        // el conversationHistory es el contexto de la charla.
        public async Task<Result<JsonElement>> SendRequestAsync(IEnumerable<JsonElement> conversationHistory)
        {
            Result<JsonElement> result;
            try
            {
                StringContent content = BuildRequestBody(conversationHistory); // lo tiene que implementar la clase concreta.

                using CancellationTokenSource cts =
                    new CancellationTokenSource(Client.Timeout);

                Logger.LogInformation("Sending request to '{model}'", lLMProviderOptions.Model);

                // respuesta del llm con un post
                HttpResponseMessage response = await Client.PostAsync(
                    lLMProviderOptions.RelativeEndpoint, 
                    content, cts.Token);

                // ¿Qué nos respondió el LLM?
                string responseContent = await response.Content.ReadAsStringAsync(cts.Token);

                if (response.IsSuccessStatusCode)
                {
                    Logger.LogInformation(
                        "Response received from model '{model}': \n {response}",
                        lLMProviderOptions.Model,
                        responseContent);

                    return ProcessResponse(responseContent);
                }
                else// si no fue exitoso
                {
                    result = Result<JsonElement>.Fail(responseContent);
                }
                // lo que me retorna el llm puede ser un Json o un texto plano, eso quien lo sabe? el proveedor completo.



            }
            catch(Exception ex)
            {
                result = Result<JsonElement>.Fail(GetErrorMessage(ex));
            }

            return result;
        }

        protected abstract StringContent BuildRequestBody(IEnumerable<JsonElement> conversationHistory);

        // un método para que proceses la respuesta y me des el resultado
        protected abstract Result<JsonElement> ProcessResponse(
            string responseContent); 

        protected string GetErrorMessage(Exception ex) => ex switch
        {
            TaskCanceledException tcEx when
                tcEx.InnerException is TimeoutException ||
                tcEx.CancellationToken.IsCancellationRequested =>
                string.Format(Messages.ModelTimeOutTemplate,
                    ModelName, Client.Timeout.TotalMinutes),
            HttpRequestException httpEx =>
                string.Format(Messages.CommunicationErrorTemplate,
                    GetType().Name, httpEx.Message),
            JsonException jsonEx =>
                string.Format(Messages.ProcesingErrorTemplate,
                    GetType().Name, jsonEx.Message),
            _ => string.Format(Messages.UnexpectedError, GetType().Name, ex.Message)
        };

        // en los headers con ollama no son necesarios, por eso es virtual
        protected virtual void ConfigureHeader(HttpClient client) { }

    }
}
