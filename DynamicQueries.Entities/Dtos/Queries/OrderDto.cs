namespace DynamicQueries.Entities.Dtos.Queries
{
    public class OrderDto(string fileName,
                          string orderType)
    {
        public string FileName => fileName;
        public string OrderType => orderType;
    }
}