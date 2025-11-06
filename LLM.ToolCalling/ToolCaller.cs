using LLM.Abstractions.Extensions;
using LLM.Abstractions.Interfaces;
using LLM.Abstractions.Models;
using LLM.Abstractions.Resources;
using Microsoft.Extensions.Logging;
using Results;
using System.Text;
using System.Text.Json;

namespace LLM.ToolCalling
{
    // necesita el provider, pq ps lo necesita para hablar con él.
    internal class ToolCaller(
        ILLMProvider provider,
        ILogger<ToolCaller> logger) : IToolCaller
    {
        public async Task<Result<ToolCallResponse>> CallAsync(ToolCallRequest request)
        {
            // por cada petición que hagamos, posiblemente tenemos una respuesta, dame el clima de puebla, damelo, pero el le dice,
            // dejame consultar el clima de puebla, ejecuta esta herramienta, ese mensaje, dejame consultar el clima de puebla, lo debemos capturar, forma parte
            // de todas las respuestas que nos hace el llm.
            // por cada petición guardaremos la respuesta
            StringBuilder combinedResponse = new();
            int maxIterations = 10; //hasta cuantats veces, tendremos un limite de 10
            int currentIteration = 0;
            try
            {
                while (currentIteration < maxIterations)
                {
                    currentIteration++; // incrementamos
                    logger.LogInformation($"starting iteration {currentIteration}");


                    Result<JsonElement> sendMessageResult = await provider.SendRequestAsync
                        (request.ConversationHistory, request.Tools);
                    if (!sendMessageResult.IsSuccess)
                        return Result<ToolCallResponse>.Fail(
                            sendMessageResult.ErrorMessage);

                    JsonElement responseMessage = sendMessageResult.Value;// RESPUESTA DEL LLM PROVIDER

                    request.ConversationHistory.Add(responseMessage);

                    if (responseMessage.TryGetProperty("content", out JsonElement content))
                    {
                        string stringContent = content.ToString();
                        if (!string.IsNullOrWhiteSpace(stringContent))
                        {
                            // voy a tomar la respuesta y se la voy a agregar a las respuestas combinadas
                            combinedResponse.AppendLine(stringContent);
                        }
                        logger.LogInformation("Response content: {content}", stringContent);
                    }

                    if (responseMessage.TryGetProperty("tool_calls", out var toolsCalls) &&
                        toolsCalls.GetArrayLength() > 0)
                    {
                        logger.LogInformation("Iteration {iteration}: {count} function(s) to execute",
                            currentIteration, toolsCalls.GetArrayLength());

                        foreach (JsonElement call in toolsCalls.EnumerateArray())
                        {
                            // primero obtendremos la info de la función.
                            JsonElement function = call.GetProperty("function"); // ahí en este objeto
                            string functionName = function.GetProperty("name").GetString();
                            JsonElement arguments = function.GetProperty("arguments").ParseArguments(); // los argumentos pudieran llegar comoo objeto json o un string comn
                            /*
                             * {"city": "puebla"} // este es object, este es el que ocupamos
                             * "{"city": "puebla"}" // este es string 
                             * */

                            string callId = call.TryGetProperty("id", out JsonElement id)
                                ? id.GetString() : null;

                            logger.LogWarning("Executing function: '{functionName}' with '{arguments}'\n",
                                functionName, arguments.GetRawText());

                            string toolcallResult = await request.ToolExecutor(functionName, arguments);

                            JsonElement toolCallResultElement = provider.CreateToolCallResultMessage(callId, functionName, toolcallResult);

                            request.ConversationHistory.Add(toolCallResultElement);

                            logger.LogWarning("Tool result: {result}\n", toolcallResult);


                        }
                    }
                    else // si no viene el tool call, es que ya terminamos
                    {
                        logger.LogInformation("No more tool calls, finishing at iteration {iteration}", currentIteration);
                        break;
                    }
                }

                if (currentIteration >= maxIterations)
                {
                    logger.LogWarning("Reached maximum iterations {maxIterations}, stoping", maxIterations);
                }
                return Result<ToolCallResponse>.Ok(new ToolCallResponse(combinedResponse.ToString(), request.ConversationHistory));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error in iterative tool calling");
                return Result<ToolCallResponse>.Fail(string.Format(Messages.UnexpectedError,
                    nameof(ToolCaller), ex.Message));
            }
        }
    }
}
