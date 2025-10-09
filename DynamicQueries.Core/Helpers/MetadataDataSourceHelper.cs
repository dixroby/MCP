using DynamicQueries.BusinessObjects.Dtos;
using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.Entities.Dtos.Metadata;

namespace DynamicQueries.Core.Helpers
{
    internal static class MetadataDataSourceHelper
    {
        public static IEnumerable<DataSourceInfoDto> GetDataSourcesInfo(
            IExecuteQueryRepository repository)
        {
            List<DataSourceInfoDto> Result = [];

            IEnumerable<DataSourceDto> DataSources = repository.GetAllDataSources();

            foreach (DataSourceDto dataSource in DataSources)
            {
                var properties = dataSource.Queryable.ElementType.GetProperties()
                    .ToDictionary(p => p.Name, p => p.PropertyType.Name);

                Result.Add(new DataSourceInfoDto(dataSource.Name, dataSource.Description,
                    [..dataSource.Properties.Select(P => new FieldInfoDto(P.Name, properties[P.Name],
                    P.Description))
                    ]));
            }
            return Result;
        }
    }
}
