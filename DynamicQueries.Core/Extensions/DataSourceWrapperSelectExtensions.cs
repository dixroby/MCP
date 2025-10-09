using DynamicQueries.BusinessObjects.DynamicLinqModels;
using DynamicQueries.Core.Helpers;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicQueries.Core.Extensions
{
    internal static class DataSourceWrapperSelectExtensions
    {
        // Select(p => new object[] {p.FieldN1Value, ... , p.FieldN2value};
        // que va a hacer este método de extensión, agregar los campos del select
        public static QueryableProjection AddSelectedFields(this DataSourceWrapper wrapper,
            string[] fieldNames)
        {
            ParameterExpression Parameter = wrapper.ParameterExpression;

            PropertyInfo[] SelectedProperties;
            if (fieldNames?.Length > 0)
            {
                SelectedProperties = [.. fieldNames
                    .Select(name => wrapper.GetPropertyInfo(name))
                    .Where(p => p != null)];
            }
            else
            {
                SelectedProperties = wrapper.GetAllPropertiesInfo();
            }
            // "{p.FieldN1Value} = unaryExpresion"
            IEnumerable<UnaryExpression> Bindings = SelectedProperties
                .Select(prop =>
                {
                    // EL PARAMETER CON SU PROPIEDAD, p.FieldValue1
                    MemberExpression PropertyAccess = Expression.Property(Parameter, prop);

                    UnaryExpression BindingExpression;

                    if (prop.PropertyType == typeof(double))
                    {

                        // primero es de decimal y luego a objet.
                        BindingExpression = Expression.Convert(PropertyAccess,
                            typeof(decimal));
                        BindingExpression = Expression.Convert(PropertyAccess,
                            typeof(object));

                    }

                    else
                    {
                        // la expresión binding 
                        BindingExpression = Expression.Convert(PropertyAccess,
                            typeof(object));
                    }
                    return BindingExpression;

                    // cuando trabajamos con colecciones, en memoria, no hay pepe, con un motor con base de datos
                    // se manejan campos como single, float, real, etc, que cuando lo queramos convertir a double
                });
            // new object[] {p.FieldN1Value, ... , p.FieldN2value};
            NewArrayExpression NewArray =
                Expression.NewArrayInit(typeof(object), Bindings);

            LambdaExpression Lambda =
                Parameter.MakeFuncLambdaExpression(NewArray, typeof(object[]));


            MethodInfo SelectMethod = QueryableHelper.GetSelectMethodInfo(wrapper.ElementType, typeof(object[]));

            // parametros, son el wrapper DataSource.Queryable
            IQueryable<object[]> QueryableResult = (IQueryable<object[]>)SelectMethod
                .Invoke(null, [wrapper.DataSource.Queryable, Lambda]); // va atrabajar el queryable del repositorio, la lambda 

            return new QueryableProjection(QueryableResult, SelectedProperties);
            // nostoros generemoas el Properties y el executionQueryResult, pero el Queryable lo expone el repositorio,
        }
    }
}
