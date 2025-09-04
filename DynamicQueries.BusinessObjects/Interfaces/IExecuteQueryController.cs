using DynamicQueries.BusinessObjects.Results;
using DynamicQueries.Entities.Dtos.Queries;
using System.Dynamic;

namespace DynamicQueries.BusinessObjects.Interfaces
{
    public interface IExecuteQueryController
    {
        Task<Result<IEnumerable<ExpandoObject>>> HandleRequestAsync(QueryDto queryDto);
    }
}