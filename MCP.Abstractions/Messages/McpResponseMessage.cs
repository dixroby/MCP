using System.Text.Json.Serialization;

namespace MCP.Abstractions.Messages
{
    public class McpResponseMessage : McpMessage
    {
        // se devuelven las respuestas como exitoso o de error

        //aqui se envía el resultado exitoso
        [JsonPropertyName("result")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Result { get; set; }

        //aqui se envía el resultado error
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Error { get; set; }

        public static McpResponseMessage CreateErrorResponse(object messageId,
                                                         int code,
                                                         string message,
                                                         object data = null)
        => new McpResponseMessage
        {
            Id = messageId,
            Error = new
            {
                code = code,
                message = message,
                data = data
            }
        };
    }
}
