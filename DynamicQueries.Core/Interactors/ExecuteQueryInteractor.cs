using DynamicQueries.BusinessObjects.Dtos;
using DynamicQueries.BusinessObjects.DynamicLinqModels;
using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.BusinessObjects.Results;
using DynamicQueries.Core.Extensions;
using DynamicQueries.Core.Resources;
using DynamicQueries.Entities.Dtos.Queries;

namespace DynamicQueries.Core.Interactors
{
    internal class ExecuteQueryInteractor(
        IExecuteQueryRepository repository,
        IExecuteQueryOutputPort outputPort): IExecuteQueryInputPort
    {
        // este método armará un queryable proyection, que se lo mandará al outputPort.

        public async Task HandleQueryAsync(QueryDto queryDto)
        {
            Result<QueryableProjection> HandleQueryResult;

            DataSourceDto DataSource = repository.GetDataSourceByName(queryDto.DataSource);

            if(DataSource != null)
            {
                try
                {
                    DataSourceWrapper Wrapper = new(DataSource);
                    var QueryableProjection = Wrapper
                        .AddFilters(queryDto.Filters)
                        .AddOrderings(queryDto.Orders)
                        .AddSelectedFields(queryDto.FieldNames);
                    // EL QUERYABLE PROJECTION, TIENE EL QUERYABLE QUE LE VAMOS A MANDAR AL REPOSITORIO
                    // resultado de la ejecución de la consulta

                    // queryable que fue armado en el SelectedFields
                    QueryableProjection.ExecutionQueryResult = await repository.GetDataAsync(QueryableProjection.Queryable);

                    // aquí lo que tuvimos
                    HandleQueryResult = Result<QueryableProjection>.Ok(QueryableProjection);
                }
                catch (Exception ex)
                {
                    HandleQueryResult = Result<QueryableProjection>.Fail(ex.Message);
                }
            }
            else
            {
                HandleQueryResult = Result<QueryableProjection>.Fail(string.Format(
                    Messages.DataSourceNotFoundTemplate,
                    queryDto.DataSource));
            }

            await outputPort.PresentAsync(HandleQueryResult);

        }
    }
}
