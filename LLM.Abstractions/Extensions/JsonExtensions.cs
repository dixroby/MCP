using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LLM.Abstractions.Extensions
{
    public static class JsonExtensions
    {
        // SERIALIZACIÓN EN CAMEL CASE, 
        private static readonly JsonSerializerOptions s_jsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // los mensajes a llm trabajan con camel casing, role y content en camel, tiene que establecer opciones
        public static JsonElement ToJsonElement<T>(this T value) =>
            JsonSerializer.SerializeToElement(value, s_jsonOptions);
    }
}
