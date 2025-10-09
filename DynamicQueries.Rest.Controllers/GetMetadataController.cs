using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.Entities.Dtos.Metadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DynamicQueries.Rest.Controllers
{
    public static class GetMetadataController
    {
        public static IEndpointRouteBuilder UseDynamicQueriesMetadataEndpoints(
            this IEndpointRouteBuilder builder,
            string pattern = "/metadata")
        {
            builder.MapGet(pattern,
                (IGetMetadataInputPort inputPort) =>
                    TypedResults.Ok(inputPort.GetDataSourcesMetadata())
                    )
                .Produces<SchemaMetadataDto>(StatusCodes.Status200OK);

            return builder;
        }
    }
}
