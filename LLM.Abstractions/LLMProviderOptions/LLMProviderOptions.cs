namespace LLM.Abstractions.LLMProviderOptions
{
    public class LLMProviderOptions
    {
        public const string SectionKey = nameof(LLMProviderOptions);

        public string Model { get; set; }
        public string BaseUrl { get; set; }
        public string RelativeEndpoint { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public string AuthenticationHeaderName { get; set; }
        public string AuthenticationHeaderValue { get; set; }



    }
}
