using DynamicQueries.BusinessObjects.Dtos;

namespace DynamicQueries.BusinessObjects.Interfaces
{
    public interface IRepository
    {
        DataSourceDto GetDataSourceByName(string name);
        IEnumerable<DataSourceDto> GetAllDataSources();
        Task<IEnumerable<object[]>> GetDataAsync( IQueryable<object[]> queryable);
    }
}