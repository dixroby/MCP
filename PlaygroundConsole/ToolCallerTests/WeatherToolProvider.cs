using LLM.Abstractions.Models;
using System.ComponentModel;
using System.Text.Json;

namespace PlaygroundConsole.ToolCallerTests
{
    internal class WeatherToolProvider
    {
        /// implementar las tools
        private static Tool s_weatherTool => new()
        {
            Name = "get_weather",
            Description = "Get current weather information for a specific city",
            InputSchema = new
            {
                type = "object",
                properties = new
                {
                    city = new
                    {
                        type = "string",
                        description = "Name of the city"
                    },

                },
                required = new[] { "city" }
            }
        };

        public static string ToolName = s_weatherTool.Name;

        public static Task<Tool> GetToolAsync() =>
        Task.FromResult(s_weatherTool);

        public static Func<JsonElement, Task<string>> HandleToolCallAsync => args =>
        {
            string result;

            try
            {
                var city = args.GetProperty("city").ToString();
                result = $"En {city} está soleado con 25 grados centígrados";
            }
            catch (Exception ex)
            {
                result = $"Error obteniendo el clima: {ex.Message}";
            }

            return Task.FromResult(result);
        };

    }
}
