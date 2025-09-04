using System.Reflection;

namespace DynamicQueries.BusinessObjects.DynamicLinqModels
{
    public class QueryableProyection(IQueryable<object[]> queryable,
                                     PropertyInfo[] properties)
    {
        public IQueryable<object[]> Queryable => queryable;
        public PropertyInfo[] Properties => properties;
    }
}