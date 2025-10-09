using System.Reflection;

namespace DynamicQueries.BusinessObjects.DynamicLinqModels
{
    // esta clase es para almacenar ,
    public class QueryableProjection(IQueryable<object[]> queryable,
        PropertyInfo[] properties)// el primer elemento tiene la propiedad del primer valor, el segundo elemento tiene 
                                  // el valor del elemento del arreglo.
    {
        public IQueryable<object[]> Queryable => queryable; //ESTO REPRESENTA UNA CONSULTA LINQ, QUE VA A DEVOLVER 
        // SI AQUÍ ES UNA PIZZA DTO, ES UNA CONSULTA LINQ CON MUCHOS PIZZADTO.
        // UNA COLECCIÓN DE ARREGLO DE OBJETOS, CADA ELEMENTO DEL ARREGLO, TENDRÁ EL VALOR DE UN CAMPO DE LA CONSULTA
        // dame el id, name y basePrice, tendrá la colección del valor id, name y basePrice.
        // estos son los valores.

        public PropertyInfo[] Properties => properties;
        // el primer elemento corresponde al valor 0, el elemento 1, corresponde al valor uno, tiene los nombres de las propiedades.
        // tiene los valores mismos, como una hoja de excel,  estos son los headers

        // REPRESENTA el resultado de la ejecución de la consulta.
        public IEnumerable<object[]> ExecutionQueryResult { get; set; }
    }
    // esta es la base de la consulta, cuando ejecute yo la 
}
// aquí va a tener todo el queryable