using DynamicQueries.BusinessObjects.DynamicLinqModels;
using DynamicQueries.Core.Helpers;
using DynamicQueries.Entities.Dtos.Queries;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicQueries.Core.Extensions
{
    internal static class DataSourceWrapperWhereExtensions
    {
        // Where(p => p.FilterFile1 condition | Value && ||| 
        public static DataSourceWrapper AddFilters(
            this DataSourceWrapper wrapper, FilterDto[] filters)
        {
            // aquí solo construimos el where.
            DataSourceWrapper Result = wrapper; // vamos a retornar el wrapper como lo trajimos si no nos dieron filtros.

            if(filters?.Length > 0)
            {
                ParameterExpression Parameter = wrapper.ParameterExpression; // necesito armar las expresiones de Filtro
                Expression FilterExpressions = null;
                for(int i = 0; i < filters.Length; i++)
                {
                    Expression FilterExpression =
                        Parameter.MakePropertyFilterExpression(filters[i].FieldName,
                        filters[i].Operation, filters[i].Value);

                    if(i == 0)
                    {
                        // si es  1, solo es el filter expressión.
                        FilterExpressions = FilterExpression;
                    }
                    else
                    {
                        // del filtro anterior
                        FilterExpressions = FilterExpressions.MakeJoinExpression(filters[i - 1].JoinWithNext, FilterExpression);
                    }
                    // cumple o no cuimple la información
                    LambdaExpression Lambda = Parameter.MakeFuncLambdaExpression(FilterExpressions, typeof(bool));

                    MethodInfo WhereMethod = QueryableHelper.GetWhereMethodInfo(wrapper.ElementType);

                    Result.DataSource.Queryable = (IQueryable) WhereMethod
                        .Invoke(null, [wrapper.DataSource.Queryable, Lambda]);
                }
            
            }


            return Result;
        }
    }
}
