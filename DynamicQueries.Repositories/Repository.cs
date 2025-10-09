using DynamicQueries.BusinessObjects.Dtos;
using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.Repositories.DataSource;

namespace DynamicQueries.Repositories
{
    internal class Repository : IExecuteQueryRepository
    {
        private static readonly DataSourceDto[] s_dataSources = [
            PizzaDataSource.Metadata, ToppingsDataSource.Metadata,
            ];

        public IEnumerable<DataSourceDto> GetAllDataSources() => s_dataSources;

        public Task<IEnumerable<object[]>> GetDataAsync(IQueryable<object[]> queryable)
            => Task.FromResult<IEnumerable<Object[]>>([.. queryable]);

        public DataSourceDto GetDataSourceByName(string name) => s_dataSources.FirstOrDefault(ds =>
            ds.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    }
}
