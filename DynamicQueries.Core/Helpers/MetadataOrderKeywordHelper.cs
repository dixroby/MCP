namespace DynamicQueries.Core.Helpers
{
    public static class MetadataOrderKeywordHelper
    {
        public static Dictionary<string, string> GetOrderKeywords() =>
            new Dictionary<string, string>
            {
                ["ascending"] = "asc",
                ["descendiing"] = "desc"
            };
    }
}
