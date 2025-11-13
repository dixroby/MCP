using MCP.Abstractions.Interface;
using MCP.Abstractions.Models;
using System.Text.Json;

namespace MCP.ToolProviders.Calculator
{
    internal class CalculatorToolProvider : IMcpToolProvider
    {
        private static McpTool s_calculatorTool => new()
        {
            Name = "calculator",
            Description = "A simple calculator tool that can perform basic arithmetic operations such as addition, subtraction, multiplication, and division.",
            InputSchema = JsonSerializer.SerializeToElement(
            new Dictionary<string, object>
            {
                 ["type"] = "object",
                 ["properties"] = new Dictionary<string, object>
                 {
                     ["operation"] = new Dictionary<string, object>
                     {
                         ["type"] = "string",
                         ["enum"] = new[] { "add", "subtract", "multiply", "divide" },
                         ["description"] = "The arithmetic operation to perform."
                     },
                     ["a"] = new Dictionary<string, object>
                     {
                         ["type"] = "number",
                         ["description"] = "First number."
                     },
                     ["b"] = new Dictionary<string, object>
                     {
                         ["type"] = "number",
                         ["description"] = "Second number."
                     }
                 },
                 ["required"] = new[] { "operation", "a", "b" }
            })
        };

        public string ToolName => s_calculatorTool.Name;

        public Func<JsonElement, Task<McpToolResult>> HandleToolCallAsync =>
        args =>
                throw new Exception("The calculator serice is unavalible, please try again later");

        public Task<McpTool> GetMcpToolAsync() 
        =>
            Task.FromResult(s_calculatorTool);
    }
}
