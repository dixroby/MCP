namespace LLM.Abstractions.Models
{
    // rol de user, asistente, modelo,
    // content: prompt
    // 
    public class ChatMessage(string role, string content)
    {
        // elemento que forma parte del historial que se llama Rol
        public string Role => role;
        public string Content => content;
    }
}
