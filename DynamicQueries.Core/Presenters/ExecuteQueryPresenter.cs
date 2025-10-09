using DynamicQueries.BusinessObjects.DynamicLinqModels;
using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.BusinessObjects.Results;
using System.Dynamic;

namespace DynamicQueries.Core.Presenters
{
    internal class ExecuteQueryPresenter : IExecuteQueryOutputPort
    {
        public Result<IEnumerable<ExpandoObject>> Result { get; private set; }

        public Task PresentAsync(Result<QueryableProjection> queryExecutionResult)
        {
            if (queryExecutionResult.IsSuccess)
            {
                Result = Result<IEnumerable<ExpandoObject>>.Ok(ConvertToExpando(queryExecutionResult.SuccessValue));
            }
            else
            {
                Result = Result<IEnumerable<ExpandoObject>>.Fail(
                    queryExecutionResult.ErrorMessage);
            }

            return Task.CompletedTask;
        }

        IEnumerable<ExpandoObject> ConvertToExpando(QueryableProjection projection)
        {
            foreach (object[] PropertyValues in projection.ExecutionQueryResult)
            {
                IDictionary<string, object> ExpandoDict = new ExpandoObject();

                for(int i = 0; i < projection.Properties.Length; i++)
                {
                    ExpandoDict[projection.Properties[i].Name] = PropertyValues[i];
                }
                yield return (ExpandoObject)ExpandoDict;
;            }
        }
    }
}
