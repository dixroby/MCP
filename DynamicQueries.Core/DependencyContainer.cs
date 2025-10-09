using DynamicQueries.BusinessObjects.Interfaces;
using DynamicQueries.Core.Interactors;
using DynamicQueries.Core.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicQueries.Core
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDynamicQueriesServices(this IServiceCollection services)
        {
            services.AddScoped<IExecuteQueryInputPort, ExecuteQueryInteractor>();
            services.AddScoped<IExecuteQueryOutputPort, ExecuteQueryPresenter>();

            services.AddScoped<IGetMetadataInputPort, GetMetadaInteractor>();


            return services;
        }
    }

}
