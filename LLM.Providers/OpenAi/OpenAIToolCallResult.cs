using System.Text.Json.Serialization;

namespace LLM.Providers.OpenAi
{
    public class OpenAIToolCallResult(string role,
                                      string toolCallId,
                                      string content)
    {
        [JsonPropertyName("role")]
        public string Role => role;

        [JsonPropertyName("tool_call_id")]
        public string ToolCallId => toolCallId;

        [JsonPropertyName("content")]
        public string Content => content;
    }
}