using System.Reflection;

namespace DynamicQueries.Core.Helpers
{
    internal static class QueryableHelper
    {
        // quiero un método que me devuelva la información 
        public static MethodInfo GetSelectMethodInfo(Type elementType, Type returnType)
        {
            // ESTO ES PARA CUANDO NECESITAMOS EL SELECT
            var SelectMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == "Select" &&
                    m.GetParameters().Length == 2);
            return SelectMethod.MakeGenericMethod(elementType, returnType);
        }

        public static MethodInfo GetWhereMethodInfo(Type elementType)
        {
            var WhereMethod = typeof(Queryable)
                .GetMethods()
                .First(m =>
                    m.Name == "Where" &&
                    m.GetParameters().Length == 2);

            return WhereMethod.MakeGenericMethod(elementType);
        }

        // si no hay antes un orderBy, es ThenBy, y dependiendo si es asc o no, es OrderByDescending o ThenByDescending
        public static MethodInfo GetOrderMethodInfo(bool isFirstOrdering, bool isDescending, Type elementType, Type propertyType)
        {
            string MethodName = isFirstOrdering
                ? isDescending ? "OrderByDescending" : "OrderBy"
                : isDescending ? "ThenByDescending" : "ThenBy";

            var OrderMethod = typeof(Queryable)
                .GetMethods()
                .First(m =>
                    m.Name == MethodName &&
                    m.GetParameters().Length == 2);

            return OrderMethod.MakeGenericMethod(elementType, propertyType);

        }
    }
}
