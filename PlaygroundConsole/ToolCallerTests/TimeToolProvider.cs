using LLM.Abstractions.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlaygroundConsole.ToolCallerTests
{
    internal class TimeToolProvider
    {
        private static Tool s_TTimeTool =>
            new()
            {
                Name = "get_time",
                Description = "Get the current time right now",
                InputSchema = new
                {
                    type = "object",
                    properties = new { },
                    required = Array.Empty<string>(),
                }
            };

        public static string ToolName => s_TTimeTool.Name;

        public static Task<Tool> GetToolAsync() =>
            Task.FromResult(s_TTimeTool);

        public static Func<JsonElement, Task<string>> HandleToolCallAsync =>
            args =>
                Task.FromResult(DateTime.Now.ToString("yyy-MM-dd HH:mm:sss"));

    }
}