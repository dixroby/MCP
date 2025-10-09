using DynamicQueries.BusinessObjects.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicQueries.Repositories
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDynamicQueriesRepositories(this IServiceCollection services)
        {
            services.AddScoped<IExecuteQueryRepository, Repository>();

            return services;
        }
    }

}
