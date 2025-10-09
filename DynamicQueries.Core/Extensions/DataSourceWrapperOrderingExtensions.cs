using DynamicQueries.BusinessObjects.DynamicLinqModels;
using DynamicQueries.Core.Helpers;
using DynamicQueries.Core.Resources;
using DynamicQueries.Entities.Dtos.Queries;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicQueries.Core.Extensions
{
    public static class DataSourceWrapperOrderingExtensions
    {
        /*lambda con parametro, y un memberexpression(propertyAccess).
         * source
         *  .OrderBy|OrderByDescending(p => p.Property1)
         *  .ThenBy(|ThenByDescending(p => p.Property2)
         * 
         */
        public static DataSourceWrapper AddOrderings(this DataSourceWrapper wrapper,
            OrderDto[] orderings)
        {
            DataSourceWrapper Result = wrapper;

            if (orderings?.Length > 0)
            {
                ParameterExpression Parameter = wrapper.ParameterExpression;
                for (int i = 0; i < orderings.Length; i++)
                {
                    PropertyInfo Property =
                        wrapper.GetPropertyInfo(orderings[i].FieldName) ??
                        throw new ArgumentException(string.Format(
                            Messages.PropertyNotFoundTemplate));
                    // p.Filed1
                    MemberExpression PropertyAccess =
                        Expression.Property(Parameter, Property);

                    LambdaExpression OrderByLambda = Expression.Lambda(
                        PropertyAccess, Parameter);

                    MethodInfo Method = QueryableHelper.GetOrderMethodInfo(
                        i == 0, orderings[i].OrderType?.ToLower() == "desc", wrapper.ElementType, Property.PropertyType);

                    // a el aplicaselo
                    Result.DataSource.Queryable = (IQueryable)Method.Invoke(null, [Result.DataSource.Queryable]);
                }
            }
            return Result;

        }
    }
}