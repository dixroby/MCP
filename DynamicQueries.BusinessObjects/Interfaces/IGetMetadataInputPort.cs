using DynamicQueries.Entities.Dtos.Metadata;

namespace DynamicQueries.BusinessObjects.Interfaces
{
    public interface IGetMetadataInputPort
    {
        SchemaMetadataDto GetDataSourcesMetadata();
    }
}
