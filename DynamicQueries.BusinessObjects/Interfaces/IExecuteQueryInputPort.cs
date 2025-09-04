using System.Dynamic;

namespace DynamicQueries.BusinessObjects.Interfaces
{
    public interface IExecuteQueryInputPort
    {
        Task<IEnumerable<ExpandoObject>> ExecuteQueryAsync(string query);
    }
}