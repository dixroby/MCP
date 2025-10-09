using DynamicQueries.Entities.Dtos.Metadata;

namespace DynamicQueries.Core.Helpers
{
    internal static class MetadataOperatorSupportHelper
    {
        public static Dictionary<string, OperatorSupportDto> GetOperatorSupport() =>
            new Dictionary<string, OperatorSupportDto>
            {
                ["string"] = new OperatorSupportDto(
                    ["==", "!=", "contains"]),
                ["decimal"] = new OperatorSupportDto(
                    ["==", "!=", "<", "<=", ">", ">="]),
                ["int"] = new OperatorSupportDto(
                    ["==", "!=", "<", "<=", ">", ">="]),
                ["bool"] = new OperatorSupportDto(
                    ["==", "!="]),
            };
    }
}
