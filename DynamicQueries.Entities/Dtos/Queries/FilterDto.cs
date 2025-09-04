namespace DynamicQueries.Entities.Dtos.Queries
{
    public class FilterDto(string fieldName,
                           string operation,
                           string value,
                           string joinWhithNext)
    {
        public string FieldName => fieldName;
        public string Operation => operation;
        public string Value => value;
        public string JoinWhithNext => joinWhithNext;
    }
}