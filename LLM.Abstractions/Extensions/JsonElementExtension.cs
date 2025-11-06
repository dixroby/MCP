using System.Text.Json;

namespace LLM.Abstractions.Extensions
{
    public static class JsonElementExtension
    {
        public static JsonElement ParseArguments(this JsonElement element) =>
            element.ValueKind switch
            {
                JsonValueKind.String => JsonDocument.Parse(element.GetString()).RootElement,
                JsonValueKind.Object => element,
                _ => throw new InvalidOperationException()
            };
    }
}
