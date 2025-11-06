using LLM.Abstractions.Models;
using Results;

namespace LLM.Abstractions.Interfaces
{
    public interface IToolCaller
    {
        Task<Result<ToolCallResponse>> CallAsync(ToolCallRequest request);

    }
}
