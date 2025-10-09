using DynamicQueries.BusinessObjects.DynamicLinqModels;
using DynamicQueries.BusinessObjects.Results;
using System.Dynamic;

namespace DynamicQueries.BusinessObjects.Interfaces
{
    public interface IExecuteQueryOutputPort
    {
        Result<IEnumerable<ExpandoObject>> Result { get; }
        // puedo recibir un queryable projectión o bien un error, hubo una exepción, armate un r esult con el mensaje de error
        // armate un result con los datos de la consulta.
        Task PresentAsync(Result<QueryableProjection> queryExecutionResult);
    }
}
