using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.Entities.Dtos.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Dynamic;

namespace DynamicQueries.Rest.Controllers
{
    public static class ExecuteQueryController
    {
        public static IEndpointRouteBuilder UseDynamicQueriesEndpoint(
            this IEndpointRouteBuilder builder,
            string pattern ="/query")
        {
            builder.MapPost(pattern,
                async (QueryDto query,
                    IExecuteQueryInputPort inputPort,
                    IExecuteQueryOutputPort outputPort) =>
                {
                    IResult result;
                    await inputPort.HandleQueryAsync(query);
                    if(outputPort.Result.IsSuccess)
                    {
                        result =  TypedResults.Ok(outputPort.Result.SuccessValue);
                    }
                    else
                    {
                        result=  TypedResults.Problem(
                            detail: outputPort.Result.ErrorMessage,
                            statusCode: StatusCodes.Status400BadRequest);
                    }

                    return result;

                })
                .Produces<IEnumerable<ExpandoObject>>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

            return builder;
        }
    }
}
