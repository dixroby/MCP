namespace DynamicQueries.Entities.Dtos.Metadata
{
    public class FieldInfoDto(string name, string type, string description)
    {
        public string Name => name;
        public string Type => type;
        public string Description => description;
    }
}
