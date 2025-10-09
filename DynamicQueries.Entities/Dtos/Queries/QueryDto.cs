namespace DynamicQueries.Entities.Dtos.Queries
{
    public class QueryDto(string dataSource,
        string[] fieldNames = null,
        FilterDto[] filters = null,
        OrderDto[] orders = null)
    {
        public string DataSource => dataSource;
        public string[] FieldNames => fieldNames;
        public FilterDto[] Filters => filters;
        public OrderDto[] Orders => orders;
    }
}
