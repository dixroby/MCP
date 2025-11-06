using LLM.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaygroundConsole.ToolCallerTests
{
    internal abstract class CryptToolProviderBase
    {
        private readonly string _toolName;
        private readonly string _toolDescription;
        private readonly string _textDescription;

        protected CryptToolProviderBase(string toolName, string toolDescription, string textDescription)
        {
            _toolName = toolName ?? throw new ArgumentNullException(nameof(toolName));
            _toolDescription = toolDescription ?? throw new ArgumentNullException(nameof(toolDescription));
            _textDescription = textDescription ?? throw new ArgumentNullException(nameof(textDescription));
        }

        private Tool BuildTool() => new Tool
        {
            Name = _toolName,
            Description = _toolDescription,
            InputSchema = new
            {
                type = "object",
                properties = new
                {
                    text = new
                    {
                        type = "string",
                        description = _textDescription
                    }
                },
                required = new[] { "text" }
            }
        };

        public string ToolName => _toolName;

        public Task<Tool> GetToolAsync() => Task.FromResult(BuildTool());

        public Func<JsonElement, Task<string>> HandleToolCallAsync => async args =>
        {
            if (!args.TryGetProperty("text", out var textProp) || textProp.ValueKind != JsonValueKind.String)
            {
                throw new ArgumentException("Falta la propiedad requerida 'text' (string) en los argumentos del tool.");
            }

            var text = textProp.GetString() ?? string.Empty;
            return ExecuteTool(text);
        };

        /// <summary>
        /// Implementa aquí la lógica del tool (por ejemplo, encriptar el texto).
        /// </summary>
        protected abstract string ExecuteTool(string text);
    }
}

