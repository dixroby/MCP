using DynamicQueries.Entities.Dtos.Queries;

namespace DynamicQueries.BusinessObjects.Interfaces
{
    public interface IExecuteQueryInputPort
    {
        // el inputPort, el ExecuteQueryInputPort, expondrá un método que reciba un DTO,
        // QUE VA A retornar la lista de ExpandoObject
        Task HandleQueryAsync(QueryDto queryDto);
    }
}
