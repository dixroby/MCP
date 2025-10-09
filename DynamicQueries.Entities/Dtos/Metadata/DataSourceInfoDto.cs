namespace DynamicQueries.Entities.Dtos.Metadata
{
    public class DataSourceInfoDto(string name, string description,
        List<FieldInfoDto> fields)
    {
        public string Name => name;
        public string Description => description;
        public List<FieldInfoDto> Fields => fields;
    }
}
