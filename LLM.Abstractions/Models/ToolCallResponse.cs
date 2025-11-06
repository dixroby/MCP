using System.Text.Json;

namespace LLM.Abstractions.Models
{
    public class ToolCallResponse(string response,
                                  List<JsonElement> conversationHistory)
    {
        public string Response => response;
        public List<JsonElement> ConversationHistory => conversationHistory;
    }
}