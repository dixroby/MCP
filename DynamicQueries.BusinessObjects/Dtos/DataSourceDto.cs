namespace DynamicQueries.BusinessObjects.Dtos
{
    public class DataSourceDto(string name,
                               string description,
                               IQueryable queryable,
                               DataSourcePropertyInfoDto[] properties)
    {
        public string Name => name;
        public string Description => description;
        public IQueryable Queryable { get; set; } = queryable;
        public DataSourcePropertyInfoDto[] Properties => properties;
    }
}