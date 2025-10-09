using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.Core.Helpers;
using DynamicQueries.Entities.Dtos.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace DynamicQueries.Core.Interactors
{
    internal class GetMetadaInteractor(IExecuteQueryRepository repository) : IGetMetadataInputPort
    {
        public SchemaMetadataDto GetDataSourcesMetadata() =>
            new SchemaMetadataDto(
                MetadataOperatorSupportHelper.GetOperatorSupport(),
                MetadataOrderKeywordHelper.GetOrderKeywords(),
                MetadataDataSourceHelper.GetDataSourcesInfo(repository),
                MetadataInputSchemaHelper.GetInputSchema()
                );

    }
}
