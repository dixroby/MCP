namespace DynamicQueries.Entities.Dtos.Queries
{
    public class OrderDto(string fieldName, string orderType)
    {
        public string FieldName => fieldName;
        public string OrderType => orderType;
    }
}
