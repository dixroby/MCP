namespace DynamicQueries.BusinessObjects.Dtos
{
    public class DataSourcePropertyInfoDto(string name, string description)
    {
        // esto es para describir los campos para armar la metadata.
        public string Name => name;
        public string Description => description;
    }
}
