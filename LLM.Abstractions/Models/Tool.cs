namespace LLM.Abstractions.Models
{
    public class Tool
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object InputSchema { get; set; }
    }
}