namespace DynamicQueries.BusinessObjects.Dtos
{
    public class DataSourcePropertyInfoDto(string name,
                                           string description)
    {
        public string Name => name;
        public string Description => description;
    }
}