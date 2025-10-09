namespace DynamicQueries.Entities.Dtos.Queries
{
    public class FilterDto(string fieldName, string operation, string value,
        string joinWithNext)
    {
        public string FieldName => fieldName;
        public string Operation => operation;
        public string Value => value;
        public string JoinWithNext => joinWithNext;
    }
}
